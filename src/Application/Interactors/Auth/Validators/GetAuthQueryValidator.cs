using AluguelIdeal.Application.Exceptions;
using AluguelIdeal.Application.Interactors.Auth.Queries;
using AluguelIdeal.Application.Repositories;
using AluguelIdeal.Application.Services;
using AluguelIdeal.Domain.Entities;
using FluentValidation;

namespace AluguelIdeal.Application.Interactors.Auth.Validators
{
    public class GetAuthQueryValidator : AbstractValidator<GetAuthQuery>
    {
        public GetAuthQueryValidator(IHashingService hashingService, IUserRepository userRepository)
        {
            this.CascadeMode = CascadeMode.Stop;

            RuleFor(query => query.GrantType)
                .Equal("password")
                .OnAnyFailure(query => throw new AuthErrorException());

            RuleFor(query => query.ClientId)
                .Equal("client_id")
                .OnAnyFailure(query => throw new AuthErrorException());

            RuleFor(query => query.Email)
                .NotEmpty()
                .EmailAddress()
                .OnAnyFailure(query => throw new AuthErrorException());

            RuleFor(query => query.Password)
                .NotEmpty()
                .OnAnyFailure(query => throw new AuthErrorException());

            RuleFor(query => query)
                .MustAsync(async (query, cancellationToken) => {
                    User user = await userRepository.GetByEmailAsync(query.Email, cancellationToken);

                    if (user == null || user.DeletedAt != null)
                        return false;

                    string hashedPassword = await hashingService.HashAsync(query.Password, cancellationToken);

                    return hashedPassword.Equals(user.Password);
                })
                .OnAnyFailure(query => throw new AuthErrorException());
        }
    }
}
