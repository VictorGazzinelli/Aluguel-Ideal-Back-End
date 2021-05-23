using AluguelIdeal.Application.Interactors.Users.Commands;
using AluguelIdeal.Application.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace AluguelIdeal.Application.Interactors.Users.Handlers
{
    public class DeleteUserInteractor : IRequestHandler<DeleteUserCommand>
    {
        private readonly IUserRepository userRepository;
        public DeleteUserInteractor(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<Unit> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            await userRepository.DeleteAsync(request.Id, cancellationToken);

            return Unit.Value;
        }
    }
}
