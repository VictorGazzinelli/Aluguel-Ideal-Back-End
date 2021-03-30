using FluentValidation;

namespace AluguelIdeal.Application.Interactors.Advertisements.Validators
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
