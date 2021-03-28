using AluguelIdeal.Api.Interactors.Advertisement.Requests;
using FluentValidation;

namespace AluguelIdeal.Api.Interactors.Advertisement.Validators
{
    public class InsertAdvertisementRequestValidator : AbstractValidator<InsertAdvertisementRequest>
    {
        public InsertAdvertisementRequestValidator()
        {
            RuleFor(request => request.Title)
                .NotNull()
                .NotEmpty()
                .MaximumLength(255);
        }
    }
}
