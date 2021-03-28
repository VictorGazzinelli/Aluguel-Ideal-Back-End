using AluguelIdeal.Api.Interactors.Advertisement.Requests;
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
