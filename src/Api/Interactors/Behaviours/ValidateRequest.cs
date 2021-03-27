using AluguelIdeal.Api.Utils.Extensions;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AluguelIdeal.Api.Interactors.Behaviours
{
    public class ValidateRequest<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> requestValidators;

        public ValidateRequest(IEnumerable<IValidator<TRequest>> requestValidators)
        {
            this.requestValidators = requestValidators;
        }

        public Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            List<ValidationFailure> requestValidationFailures = requestValidators
                .Select(validator => validator.Validate(request))
                .SelectMany(validationResult => validationResult.Errors)
                .Where(validationFailure => validationFailure != null)
                .ToList();

            if (requestValidationFailures.Any())
            {
                ArgumentException argumentException = 
                    new ArgumentException($"The request of type {typeof(TRequest).Name} failed to be validated", nameof(request));
                Dictionary<string, List<string>> errorMessagesByPropName =
                    requestValidationFailures.GroupBy(failure => failure.PropertyName, failure => failure.ErrorMessage)
                    .ToDictionary(group => group.Key, group => group.ToList());
                foreach (KeyValuePair<string, List<string>> entry in errorMessagesByPropName)
                    argumentException.Data.Add(entry.Key, entry.Value);

                throw argumentException;
            }

            return next();
        }
    }
}
