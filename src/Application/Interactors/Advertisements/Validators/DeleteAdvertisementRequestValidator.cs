using AluguelIdeal.Application.Interactors.Advertisements.Requests;
using FluentValidation;

namespace AluguelIdeal.Application.Interactors.Advertisements.Validators
{
    public class DeleteAdvertisementRequestValidator : AbstractValidator<DeleteAdvertisementRequest>
    {
        public DeleteAdvertisementRequestValidator()
        {
            RuleFor(request => request.Id)
                .GreaterThan(0);
        }
    }
}
