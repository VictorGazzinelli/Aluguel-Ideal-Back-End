using AluguelIdeal.Application.Interactors.Profiles.Commands;
using AluguelIdeal.Application.Repositories;
using FluentValidation;

namespace AluguelIdeal.Application.Interactors.Profiles.Validators
{
    public class BindProfileCommandValidator : AbstractValidator<BindProfileCommand>
    {
        public BindProfileCommandValidator(IUserRepository userRepository, IRoleRepository roleRepository)
        {
            RuleFor(command => command.UserId)
                .MustAsync(async (userId, cancellationToken) => (await userRepository.GetByIdAsync(userId, cancellationToken)) != null)
                .WithMessage((command, userId) => $"Invalid {nameof(command.UserId)}");

            RuleFor(command => command.RoleId)
                .MustAsync(async (roleId, cancellationToken) => (await roleRepository.GetByIdAsync(roleId, cancellationToken)) != null)
                .WithMessage((command, roleId) => $"Invalid {nameof(command.RoleId)}");
        }
    }
}
