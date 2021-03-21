using AluguelIdeal.Api.Interactors.Advertisement.Request;
using FluentValidation;

namespace AluguelIdeal.Api.Interactors.Advertisement.Validators
{
    public class GetAdvertisementByIdRequestValidator : AbstractValidator<GetAdvertisementByIdRequest>
    {
        public GetAdvertisementByIdRequestValidator()
        {
            RuleFor(request => request.Id)
                .GreaterThan(0);
        }
    }
}
