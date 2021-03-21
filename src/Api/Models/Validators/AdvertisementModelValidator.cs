using FluentValidation;

namespace AluguelIdeal.Api.Models.Validators
{
    public class AdvertisementModelValidator : AbstractValidator<AdvertisementModel>
    {
        public AdvertisementModelValidator()
        {
            RuleFor(model => model.Title)
                .NotNull()
                .NotEmpty()
                .MaximumLength(255);
        }
    }
}
