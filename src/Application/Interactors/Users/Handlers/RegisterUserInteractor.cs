using AluguelIdeal.Application.Exceptions;
using AluguelIdeal.Application.Interactors.Users.Commands;
using AluguelIdeal.Application.Repositories;
using AluguelIdeal.Application.Services;
using AluguelIdeal.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace AluguelIdeal.Application.Interactors.Users.Handlers
{
    public class RegisterUserInteractor : IRequestHandler<RegisterUserCommand>
    {
        private readonly IUserRepository userRepository;
        private readonly IHashingService hashingService;
        public RegisterUserInteractor(IUserRepository userRepository, IHashingService hashingService)
        {
            this.userRepository = userRepository;
            this.hashingService = hashingService;
        }

        public async Task<Unit> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            User currentUser = await userRepository.GetByIdAsync(request.Id, cancellationToken);

            if (currentUser.HasRegistered())
                throw new UserHasAlreadyBeenRegisteredException();

            currentUser = new User()
            {
                Id = request.Id,
                Name = request.Name,
                Email = request.Email,
                Phone = request.Phone,
                Password = await hashingService.HashAsync(request.Password, cancellationToken)
            };

            await userRepository.UpdateAsync(currentUser, cancellationToken);

            return Unit.Value;
        }
    }
}
