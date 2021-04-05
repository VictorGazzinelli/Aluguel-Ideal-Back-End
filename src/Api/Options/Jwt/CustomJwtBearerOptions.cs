using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using System;

namespace AluguelIdeal.Api.Options.Jwt
{
    public static class CustomJwtBearerOptions 
    {
        private static readonly JwtBearerEvents jwtBearerEvents = new CustomJwtBearerEvents();

        public static Action<JwtBearerOptions> GetSetupAction(IConfiguration configuration)
        {
            return jwtBearerOptions =>
            {
                jwtBearerOptions.TokenValidationParameters = new CustomJwtTokenValidationParameters(configuration.GetSection("Secret").Value);
                jwtBearerOptions.Events = jwtBearerEvents;
            };
        }
    }
}
