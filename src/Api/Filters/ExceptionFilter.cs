using AluguelIdeal.Api.Controllers.Models.Responses.Http;
using AluguelIdeal.Application.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AluguelIdeal.Api.Filters
{
    public class ExceptionFilter : IAsyncExceptionFilter
    {
        private readonly IDictionary<Type, Action<ExceptionContext>> exceptionHandlers;
        private readonly ILogger logger;

        public ExceptionFilter(ILoggerFactory loggerFactory)
        {
            this.logger = loggerFactory.CreateLogger<ExceptionFilter>();
            exceptionHandlers = new Dictionary<Type, Action<ExceptionContext>>
            {
                [typeof(AggregateNotFoundException)] = HandleAggregateNotFoundException,
                [typeof(AuthErrorException)] = HandleAuthErrorException
            };
        }


        public Task OnExceptionAsync(ExceptionContext context)
        {
            LogExceptionContext(context);

            Type exceptionType = context.Exception.GetType();
            if (!exceptionHandlers.ContainsKey(exceptionType))
                HandleUnkownException(context);
            else
                exceptionHandlers[exceptionType](context);

            return Task.CompletedTask;
        }

        private void LogExceptionContext(ExceptionContext context)
        {
            logger.LogError($"An exception occured!{Environment.NewLine}" +
                            $"TraceIdentifier:{context.HttpContext.TraceIdentifier}{Environment.NewLine}" +
                            context.Exception.ToString());
        }

        private void HandleAggregateNotFoundException(ExceptionContext context)
        {
            AggregateNotFoundException aggregateNotFoundException =
                context.Exception as AggregateNotFoundException;

            context.Result = new ErrorResponse(aggregateNotFoundException).AsObjectResult();

            context.ExceptionHandled = true;
        }
        private void HandleAuthErrorException(ExceptionContext context)
        {
            context.Result = new BadRequestObjectResult(new { error = "invalid_request" });

            context.ExceptionHandled = true;
        }

        private void HandleUnkownException(ExceptionContext context)
        {
            context.Result = new ErrorResponse(context.Exception).AsObjectResult();

            context.ExceptionHandled = true;
        }
    }
}
