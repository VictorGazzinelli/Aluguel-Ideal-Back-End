using AluguelIdeal.Api.Controllers.Models.Responses.Http;
using AluguelIdeal.Api.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace AluguelIdeal.Api.Options.Mvc
{
    public static class CustomMvcOptions 
    {
        public static Action<MvcOptions> SetupAction =>
            CustomMvcOptionsSetupAction;

        private static void CustomMvcOptionsSetupAction(MvcOptions mvcOptions)
        {
            mvcOptions.Filters.Add(new ProducesResponseTypeAttribute(typeof(BadRequestResponse), StatusCodes.Status400BadRequest));
            mvcOptions.Filters.Add(new ProducesResponseTypeAttribute(typeof(ErrorResponse), StatusCodes.Status401Unauthorized));
            mvcOptions.Filters.Add(new ProducesResponseTypeAttribute(typeof(ErrorResponse), StatusCodes.Status403Forbidden));
            mvcOptions.Filters.Add(new ProducesResponseTypeAttribute(typeof(ErrorResponse), StatusCodes.Status404NotFound));
            mvcOptions.Filters.Add(new ProducesResponseTypeAttribute(typeof(ErrorResponse), StatusCodes.Status500InternalServerError));
            mvcOptions.Filters.Add<ValidationFilter>();
            mvcOptions.Filters.Add<ExceptionFilter>();
        }
    }
}
