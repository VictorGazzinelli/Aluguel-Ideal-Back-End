using FluentValidation;
using FluentValidation.Results;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AluguelIdeal.Api.Interactors.Behaviours
{
    public class ValidateRequest<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> validators;

        public ValidateRequest(IEnumerable<IValidator<TRequest>> validators)
        {
            this.validators = validators;
        }

        public Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            List<ValidationFailure> failures = validators
                .Select(validator => validator.Validate(request))
                .SelectMany(validationResult => validationResult.Errors)
                .Where(validationFailure => validationFailure != null)
                .ToList();

            if (failures.Any())
                throw new BadRequestException(
                    data: failures.ToDictionary(failure => failure.PropertyName, failure => failure.ErrorMessage),
                    message: $"The given {typeof(TRequest).Name} is invalid");

            return next();
        }
    }
}
