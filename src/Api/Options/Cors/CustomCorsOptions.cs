using Microsoft.AspNetCore.Cors.Infrastructure;
using System;

namespace AluguelIdeal.Api.Options.Cors
{
    public static class CustomCorsOptions
    {
        private static readonly CorsPolicy defaultPolicy = new CorsPolicyBuilder()
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod()
            .Build();

        public static Action<CorsOptions> SetupAction =>
            CustomCorsOptionsSetupAction;

        private static void CustomCorsOptionsSetupAction(CorsOptions corsOptions)
        {
            corsOptions.AddDefaultPolicy(defaultPolicy);
        }
    }
}
