using AluguelIdeal.Api.Utils.Exceptions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace AluguelIdeal.Api.Middlewares
{
    public static class ExceptionHandlerExtension
    {
        public static Action<IApplicationBuilder> ExceptionHandler(IHostEnvironment environment)
        {
            return app =>
            {
                app.Run(async context =>
                {
                    Exception exception = context.Features.Get<IExceptionHandlerFeature>()?.Error;
                    Dictionary<string, object> exceptionInfo = new Dictionary<string, object>()
                    {
                        ["Message"] = exception?.Message,
                    };
                    if (environment.IsDevelopment())
                    {
                        exceptionInfo.Add("Type", exception?.GetType().FullName);
                        exceptionInfo.Add("StackTrace", exception?.StackTrace);
                    }
                    byte[] responseBody = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(exceptionInfo));
                    context.Response.StatusCode = exception is HttpResponseException httpResponseException ?
                            httpResponseException.Status :
                            StatusCodes.Status500InternalServerError;
                    context.Response.ContentType = "application/json";
                    await context.Response.Body.WriteAsync(responseBody, 0, responseBody.Length);
                });
            };
        }
    }
}
