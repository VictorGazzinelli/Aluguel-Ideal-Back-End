using AluguelIdeal.Application.Interactors.Users.Commands;
using AluguelIdeal.Application.Repositories;
using FluentValidation;

namespace AluguelIdeal.Application.Interactors.Users.Validators
{
    public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserCommandValidator(IUserRepository userRepository)
        {
            RuleFor(command => command.Id)
                .MustAsync(async (id, cancellationToken) => (await userRepository.GetByIdAsync(id, cancellationToken)) != null)
                .WithMessage((command, id) => $"Invalid {nameof(command.Id)}");

            RuleFor(command => command.Name)
                .NotEmpty()
                .MaximumLength(255);

            RuleFor(command => command.Email)
                .NotEmpty()
                .EmailAddress();
        }
    }
}
