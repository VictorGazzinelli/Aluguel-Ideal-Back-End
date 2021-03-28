using AluguelIdeal.Api.Utils.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.Extensions.Hosting;
using System;
using System.Net.Mime;

namespace AluguelIdeal.Api.Middlewares
{
    public class ExceptionHandlerMiddleware
    {
        protected ExceptionHandlerMiddleware()
        {

        }

        public static Action<IApplicationBuilder> ExceptionHandler(IHostEnvironment environment)
        {
            return app =>
            {
                app.Run(async context =>
                {
                    Exception exception = context.Features.Get<IExceptionHandlerFeature>()?.Error;
                    if(exception != null)
                    {
                        context.Response.StatusCode = exception.GetHttpResponseStatus();
                        context.Response.ContentType = MediaTypeNames.Application.Json;
                        byte[] responseBody = exception.GetHttpResponseBody(environment);
                        await context.Response.Body.WriteAsync(responseBody, 0, responseBody.Length);
                    }
                });
            };
        }
    }
}
