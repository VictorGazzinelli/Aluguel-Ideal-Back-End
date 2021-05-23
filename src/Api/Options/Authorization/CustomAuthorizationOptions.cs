using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using System;

namespace AluguelIdeal.Api.Options.Authorization
{
    public class CustomAuthorizationOptions : AuthorizationOptions
    {
        private static readonly AuthorizationPolicy defaultPolicy = new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme)
            .RequireAuthenticatedUser()
            .Build();

        public static Action<AuthorizationOptions> SetupAction =>
            CustomAuthorizationOptionsSetupAction;

        private static void CustomAuthorizationOptionsSetupAction(AuthorizationOptions authorizationOptions)
        {
            authorizationOptions.DefaultPolicy = defaultPolicy;
        }
    }
}
