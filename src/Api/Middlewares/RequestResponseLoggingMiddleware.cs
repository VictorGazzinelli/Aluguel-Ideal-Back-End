using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.IO;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AluguelIdeal.Api.Middlewares
{
    public class RequestResponseLoggingMiddleware
    {
        private readonly ILogger logger;
        private readonly RecyclableMemoryStreamManager recyclableMemoryStreamManager;
        private readonly RequestDelegate next;
        private readonly string[] pathsToIgnore = new string[]
        {
            "/index",
            "/swagger",
            "/favicon",
        };

        public RequestResponseLoggingMiddleware(ILoggerFactory loggerFactory, RequestDelegate next)
        {
            this.logger = loggerFactory.CreateLogger<RequestResponseLoggingMiddleware>();
            this.next = next;
            this.recyclableMemoryStreamManager = new RecyclableMemoryStreamManager();
        }

        public async Task Invoke(HttpContext context)
        {
            if (!pathsToIgnore.Any(p => context.Request.Path.ToString().StartsWith(p)))
            {
                await LogRequest(context);
                await LogResponse(context);
            }
            else
            {
                await next(context);
            }
        }

        private async Task LogRequest(HttpContext context)
        {
            context.Request.EnableBuffering();
            await using var requestStream = recyclableMemoryStreamManager.GetStream();
            await context.Request.Body.CopyToAsync(requestStream);
            LogRequest(context, requestStream);
            context.Request.Body.Position = 0;
        }


        private void LogRequest(HttpContext context, MemoryStream requestStream)
        {

            string info = $"Http Request Information:{Environment.NewLine}" +
                                    $"TraceIdentifier:{context.TraceIdentifier} " +
                                    $"Schema:{context.Request.Scheme} " +
                                    $"Host: {context.Request.Host} " +
                                    $"Path: {context.Request.Path} " +
                                    $"Method: {context.Request.Method} " +
                                    $"QueryString: {context.Request.QueryString} " +
                                    $"RequestBody: {ReadStreamInChunks(requestStream)}";
            logger.LogInformation(info);
        }

        private async Task LogResponse(HttpContext context)
        {
            var originalBodyStream = context.Response.Body;
            await using var temporary = recyclableMemoryStreamManager.GetStream();
            context.Response.Body = temporary;
            await next(context);
            context.Response.Body.Seek(0, SeekOrigin.Begin);
            string responseBodyAsJson = await new StreamReader(context.Response.Body).ReadToEndAsync();
            context.Response.Body.Seek(0, SeekOrigin.Begin);
            LogResponse(context, responseBodyAsJson);
            await temporary.CopyToAsync(originalBodyStream);
        }

        private void LogResponse(HttpContext context, string responseBodyAsJson, int? statusCode = null)
        {
            string info = $"Http Response Information:{Environment.NewLine}" +
                                               $"TraceIdentifier:{context.TraceIdentifier} " +
                                               $"Schema:{context.Request.Scheme} " +
                                               $"Host: {context.Request.Host} " +
                                               $"Path: {context.Request.Path} " +
                                               $"QueryString: {context.Request.QueryString} " +
                                               $"StatusCode: {statusCode ?? context.Response.StatusCode} " +
                                               $"Response Body: {responseBodyAsJson}";
            logger.LogInformation(info);
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
                readChunkLength = reader.ReadBlock(readChunk, 0, readChunkBufferLength);
                textWriter.Write(readChunk, 0, readChunkLength);
            } while (readChunkLength > 0);
            return textWriter.ToString();
        }
    }
}
