using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;

namespace AluguelIdeal.Api.Middlewares.Extensions
{
    public static class CustomMiddlewareExtensions
    {
        public static IApplicationBuilder UseRequestResponseLogging(this IApplicationBuilder app)
        {
            return app.UseMiddleware<RequestResponseLoggingMiddleware>();
        }

        public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder app, IHostEnvironment environment)
        {
            return app.UseExceptionHandler(ExceptionHandlerMiddleware.ExceptionHandler(environment));
        }
    }
}
