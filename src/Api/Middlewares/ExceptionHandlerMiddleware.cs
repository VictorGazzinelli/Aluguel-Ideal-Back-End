using AluguelIdeal.Api.Controllers.Models.Responses.Http;
using AluguelIdeal.Api.Utils;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.Extensions.Hosting;
using System;

namespace AluguelIdeal.Api.Middlewares
{
    public static class ExceptionHandlerMiddleware
    {
        public static Action<IApplicationBuilder> ExceptionHandler()
        {
            return app =>
            {
                app.Run(async context =>
                {
                    Exception exception = context.Features.Get<IExceptionHandlerFeature>()?.Error;
                    if (exception != null)
                        await context.WriteJsonResponseAsync(new ErrorResponse(exception));
                });
            };
        }
    }
}
