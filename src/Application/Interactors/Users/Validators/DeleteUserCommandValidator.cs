using AluguelIdeal.Application.Interactors.Users.Commands;
using AluguelIdeal.Application.Repositories;
using FluentValidation;

namespace AluguelIdeal.Application.Interactors.Users.Validators
{
    public class DeleteUserCommandValidator : AbstractValidator<DeleteUserCommand>
    {
        public DeleteUserCommandValidator(IUserRepository userRepository)
        {
            RuleFor(command => command.Id)
                .MustAsync(async (id, cancellationToken) => (await userRepository.GetByIdAsync(id, cancellationToken)) != null)
                .WithMessage((command, id) => $"Invalid {nameof(command.Id)}");
        }
    }
}
