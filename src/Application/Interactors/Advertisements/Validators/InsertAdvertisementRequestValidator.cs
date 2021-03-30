using FluentValidation;

namespace AluguelIdeal.Application.Interactors.Advertisements.Validators
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
