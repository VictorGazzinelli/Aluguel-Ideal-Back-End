using AluguelIdeal.Application.Interactors.Users.Commands;
using AluguelIdeal.Application.Repositories;
using FluentValidation;

namespace AluguelIdeal.Application.Interactors.Users.Validators
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator(IUserRepository userRepository)
        {
            RuleFor(command => command.Name)
                .NotEmpty()
                .MaximumLength(255);

            RuleFor(command => command.Email)
                .NotEmpty()
                .EmailAddress()
                .MustAsync(async (email, cancellationToken) => (await userRepository.GetByEmailAsync(email, cancellationToken)) == null)
                .WithMessage((command, email) => $"Email '{email}' taken!");
        }
    }
}
