using AluguelIdeal.Api.Responses.Advertisements;
using FluentValidation;

namespace AluguelIdeal.Api.Responses.Validators.Advertisements
{
    public class AdvertisementIdResponseValidator : AbstractValidator<AdvertisementIdResponse>
    {
        public AdvertisementIdResponseValidator()
        {
            RuleFor(response => response.Id)
            .GreaterThan(0)
            .WithName("id");
        }
    }
}
