using AluguelIdeal.Api.Controllers.Models.Responses.Http;
using FluentValidation;

namespace AluguelIdeal.Api.Controllers.Models.Validators.Http
{
    public class InternalServerErrorResponseValidator : AbstractValidator<InternalServerErrorResponse>
    {
        public InternalServerErrorResponseValidator()
        {
            RuleFor(response => response.Message)
            .NotNull()
            .NotEmpty()
            .WithName("message");
        }
    }
}
