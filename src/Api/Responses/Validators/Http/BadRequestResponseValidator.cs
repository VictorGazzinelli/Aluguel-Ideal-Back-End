using AluguelIdeal.Api.Controllers.Models.Responses.Http;
using FluentValidation;

namespace AluguelIdeal.Api.Controllers.Models.Validators.Http
{
    public class BadRequestResponseValidator : AbstractValidator<BadRequestResponse>
    {
        public BadRequestResponseValidator()
        {
            RuleFor(response => response.Errors)
            .NotNull()
            .NotEmpty()
            .WithName("errors");
        }
    }
}
