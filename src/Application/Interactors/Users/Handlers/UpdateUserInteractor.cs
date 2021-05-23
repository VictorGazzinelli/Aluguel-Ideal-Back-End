using AluguelIdeal.Application.Interactors.Users.Commands;
using AluguelIdeal.Application.Repositories;
using AluguelIdeal.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace AluguelIdeal.Application.Interactors.Users.Handlers
{
    public class UpdateUserInteractor : IRequestHandler<UpdateUserCommand>
    {
        private readonly IUserRepository userRepository;
        public UpdateUserInteractor(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<Unit> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            User user = new User()
            {
                Id = request.Id,
                Name = request.Name,
                Email = request.Email,
            };

            await userRepository.UpdateAsync(user, cancellationToken);

            return Unit.Value;
        }
    }
}
