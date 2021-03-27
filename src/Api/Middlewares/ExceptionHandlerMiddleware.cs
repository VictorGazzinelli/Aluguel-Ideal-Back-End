using AluguelIdeal.Api.Utils;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

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
                        (int statusCode, Dictionary<string, object> responseBody) = 
                            new ExceptionParser(environment).AsHttpResponse(exception);
                        byte[] responseBodyContent = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(responseBody));
                        context.Response.ContentType = "application/json";
                        await context.Response.Body.WriteAsync(responseBodyContent, 0, responseBodyContent.Length);
                    }
                });
            };
        }
    }
}
