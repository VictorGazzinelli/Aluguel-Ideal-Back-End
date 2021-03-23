using FluentValidation;

namespace AluguelIdeal.Api.Models.Validators
{
    public class ContactModelValidator : AbstractValidator<ContactModel>
    {
        public ContactModelValidator()
        {
            RuleFor(model => model.Name)
                .NotNull()
                .NotEmpty()
                .MaximumLength(255)
                .WithName("name");

            RuleFor(model => model.Email)
                .NotNull()
                .EmailAddress()
                .WithName("email");

            RuleFor(model => model.Phone)
                .NotNull()
                .WithName("phone");

        }
    }
}
