using AluguelIdeal.Api.Utils;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace AluguelIdeal.Api.Middlewares
{
    public class RequestResponseLoggingMiddleware
    {
        private readonly ILogger logger;
        private readonly RecyclableMemoryStreamManager recyclableMemoryStreamManager;
        private readonly RequestDelegate next;
        private readonly IHostEnvironment environment;

        public RequestResponseLoggingMiddleware(ILoggerFactory loggerFactory,
                                                RequestDelegate next,
                                                IHostEnvironment environment)
        {
            this.logger = loggerFactory.CreateLogger<RequestResponseLoggingMiddleware>();
            this.next = next;
            this.recyclableMemoryStreamManager = new RecyclableMemoryStreamManager();
            this.environment = environment;
        }

        public async Task Invoke(HttpContext context)
        {
            await LogRequest(context);
            await LogResponse(context);
        }

        private async Task LogRequest(HttpContext context)
        {
            context.Request.EnableBuffering();
            await using var requestStream = recyclableMemoryStreamManager.GetStream();
            await context.Request.Body.CopyToAsync(requestStream);
            logger.LogInformation($"Http Request Information:{Environment.NewLine}" +
                                   $"Schema:{context.Request.Scheme} " +
                                   $"Host: {context.Request.Host} " +
                                   $"Path: {context.Request.Path} " +
                                   $"QueryString: {context.Request.QueryString} " +
                                   $"Request Body: {ReadStreamInChunks(requestStream)}");
            context.Request.Body.Position = 0;
        }

        private async Task LogResponse(HttpContext context)
        {
            var originalBodyStream = context.Response.Body;
            await using var responseBody = recyclableMemoryStreamManager.GetStream();
            context.Response.Body = responseBody;

            string text;
            try
            {
                await next(context);

                context.Response.Body.Seek(0, SeekOrigin.Begin);
                text = await new StreamReader(context.Response.Body).ReadToEndAsync();
                context.Response.Body.Seek(0, SeekOrigin.Begin);

                logger.LogInformation($"Http Response Information:{Environment.NewLine}" +
                                   $"Schema:{context.Request.Scheme} " +
                                   $"Host: {context.Request.Host} " +
                                   $"Path: {context.Request.Path} " +
                                   $"QueryString: {context.Request.QueryString} " +
                                   $"StatusCode: {context.Response.StatusCode} " +
                                   $"Response Body: {text}");

                await responseBody.CopyToAsync(originalBodyStream);
            }
            catch(Exception exception)
            {
                (int statusCode, Dictionary<string, object> responseBody) httpResponse =
                            new ExceptionParser(environment).AsHttpResponse(exception);
                byte[] responseBodyContent = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(httpResponse.responseBody));
                text = await new StreamReader(new MemoryStream(responseBodyContent)).ReadToEndAsync();

                logger.LogInformation($"Http Response Information:{Environment.NewLine}" +
                                   $"Schema:{context.Request.Scheme} " +
                                   $"Host: {context.Request.Host} " +
                                   $"Path: {context.Request.Path} " +
                                   $"QueryString: {context.Request.QueryString} " +
                                   $"StatusCode: {httpResponse.statusCode} " +
                                   $"Response Body: {text}");

                context.Response.Body = originalBodyStream;

                throw;
            }
        }

        private static string ReadStreamInChunks(Stream stream)
        {
            const int readChunkBufferLength = 4096;
            stream.Seek(0, SeekOrigin.Begin);
            using var textWriter = new StringWriter();
            using var reader = new StreamReader(stream);
            var readChunk = new char[readChunkBufferLength];
            int readChunkLength;
            do
            {
                readChunkLength = reader.ReadBlock(readChunk,
                                                   0,
                                                   readChunkBufferLength);
                textWriter.Write(readChunk, 0, readChunkLength);
            } while (readChunkLength > 0);
            return textWriter.ToString();
        }
    }
}
