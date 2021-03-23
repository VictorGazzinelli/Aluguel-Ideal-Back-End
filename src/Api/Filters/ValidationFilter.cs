using AluguelIdeal.Api.Utils.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Linq;
using System.Threading.Tasks;

namespace AluguelIdeal.Api.Filters
{
    public class ValidationFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.ModelState.IsValid && !context.HttpContext.Response.HasStarted)
            {
                ModelStateDictionary modelStateDictionary = context.ModelState;
                object errors;
                if (modelStateDictionary.Keys.All(key => string.IsNullOrEmpty(key)))
                    errors = modelStateDictionary.SelectMany(modelStateEntryByPropName => modelStateEntryByPropName.Value.Errors.Select(e => e.ErrorMessage));
                else
                    errors = modelStateDictionary.ToDictionary(
                       keySelector: modelStateEntryByPropName => modelStateEntryByPropName.Key.ToCamelCase(),
                       elementSelector: modelStateEntryByPropName => modelStateEntryByPropName.Value.Errors.Select(e => e.ErrorMessage).ToList());
                context.Result = new BadRequestObjectResult(new { errors });
                return;
            }

            await next();
        }
    }
}
