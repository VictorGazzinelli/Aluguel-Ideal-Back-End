using AluguelIdeal.Api.Controllers.Models.Responses.Http;
using AluguelIdeal.Api.Utils.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AluguelIdeal.Api.Filters
{
    public class ValidationFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            ValidateIdsIfAny(context);
            if (!context.ModelState.IsValid)
            {
                ModelStateDictionary modelStateDictionary = context.ModelState;
                Dictionary<string, List<string>> errors = modelStateDictionary.ToDictionary(
                       keySelector: modelStateEntryByPropName => modelStateEntryByPropName.Key.ToCamelCase(),
                       elementSelector: modelStateEntryByPropName => modelStateEntryByPropName.Value.Errors.Select(e => e.ErrorMessage).ToList());
                context.Result = new BadRequestObjectResult(new BadRequestResponse() { Errors = errors });
                return;
            }

            await next();
        }

        private static void ValidateIdsIfAny(ActionExecutingContext context)
        {
            foreach (string key in context.ModelState.Keys.Where(key => key.EndsWith("id", true, null)))
                if (context.ActionArguments[key] as int? < 0)
                    context.ModelState.AddModelError(key, "less or equal to zero");
        }
    }
}
