using Microsoft.AspNetCore.Mvc;
using System;

namespace AluguelIdeal.Api.Options.ApiBehavior
{
    public static class CustomApiBehaviourOptions
    {
        public static Action<ApiBehaviorOptions> SetupAction =>
            CustomApiBehaviorOptionsSetupAction;

        private static void CustomApiBehaviorOptionsSetupAction(ApiBehaviorOptions apiBehaviorOptions)
        {
            apiBehaviorOptions.SuppressModelStateInvalidFilter = true;
            apiBehaviorOptions.SuppressMapClientErrors = true;
        }
    }
}
