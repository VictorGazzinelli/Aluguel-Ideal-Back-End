using AluguelIdeal.Api.Interactors.Advertisement.Request;
using FluentValidation;

namespace AluguelIdeal.Api.Interactors.Advertisement.Validators
{
    public class UpdateAdvertisementRequestValidator : AbstractValidator<UpdateAdvertisementRequest>
    {
        public UpdateAdvertisementRequestValidator()
        {
            RuleFor(request => request.Id)
                .GreaterThan(0);

            RuleFor(request => request.Title)
                .NotNull()
                .NotEmpty()
                .MaximumLength(255);
        }
    }
}
