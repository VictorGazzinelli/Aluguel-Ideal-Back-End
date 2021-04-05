using AluguelIdeal.Api.Controllers.Models.Responses.Http;
using AluguelIdeal.Api.Utils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace AluguelIdeal.Api.Options.Jwt
{
    public class CustomJwtBearerEvents : JwtBearerEvents
    {
        public CustomJwtBearerEvents()
        {
            this.OnTokenValidated = CustomOnTokenValidated;
            this.OnAuthenticationFailed = CustomOnAuthenticationFailed;
            this.OnForbidden = CustomOnForbidden;
            this.OnChallenge = CustomOnChallenge;
        }

        private Func<TokenValidatedContext, Task> CustomOnTokenValidated =>
            context =>
            {
                context.HttpContext.User = context.Principal;

                return Task.CompletedTask;
            };

        private Func<AuthenticationFailedContext, Task> CustomOnAuthenticationFailed =>
            context =>
            {
                Console.WriteLine("Authentication Failed!");

                return Task.CompletedTask;
            };

        private Func<ForbiddenContext, Task> CustomOnForbidden =>
            context =>
            {
                context.Response.OnStarting(async () =>
                {
                    ErrorResponse errorResponse = new ErrorResponse()
                    {
                        StatusCode = StatusCodes.Status403Forbidden,
                        Message = "Forbidden",
                    };
                    await context.HttpContext.WriteJsonResponseAsync(errorResponse);
                });

                return Task.CompletedTask;
            };

        private Func<JwtBearerChallengeContext, Task> CustomOnChallenge =>
            context =>
            {
                context.Response.OnStarting(async () =>
                {
                    ErrorResponse errorResponse = new ErrorResponse()
                    {
                        StatusCode = StatusCodes.Status401Unauthorized,
                        Message = "Unauthorized",
                    };
                    await context.HttpContext.WriteJsonResponseAsync(errorResponse);
                });

                return Task.CompletedTask;
            };
    }
}
