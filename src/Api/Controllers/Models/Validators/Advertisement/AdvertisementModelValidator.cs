using AluguelIdeal.Api.Controllers.Models.Advertisement;
using FluentValidation;

namespace AluguelIdeal.Api.Controllers.Models.Validators.Advertisement
{
    public class AdvertisementModelValidator : AbstractValidator<AdvertisementModel>
    {
        public AdvertisementModelValidator()
        {
            RuleFor(model => model.Title)
                .NotNull()
                .NotEmpty()
                .MaximumLength(255)
                .WithName("title");
        }
    }
}
