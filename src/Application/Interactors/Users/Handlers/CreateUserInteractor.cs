using AluguelIdeal.Application.Interactors.Common;
using AluguelIdeal.Application.Interactors.Users.Commands;
using AluguelIdeal.Application.Repositories;
using AluguelIdeal.Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AluguelIdeal.Application.Interactors.Users.Handlers
{
    public class CreateUserInteractor : IRequestHandler<CreateUserCommand, IdResult>
    {
        private readonly IUserRepository userRepository;
        public CreateUserInteractor(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<IdResult> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            Guid Id = Guid.NewGuid();

            User user = new User()
            {
                Id = Id,
                Name = request.Name,
                Email = request.Email,
            };

            await userRepository.CreateAsync(user, cancellationToken);

            return new IdResult() { Id = Id };
        }
    }
}
