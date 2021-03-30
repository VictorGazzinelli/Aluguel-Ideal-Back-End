using FluentValidation;

namespace AluguelIdeal.Application.Interactors.Advertisements.Validators
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
