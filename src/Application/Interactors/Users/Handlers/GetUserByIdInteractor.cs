using AluguelIdeal.Application.Dtos.Users;
using AluguelIdeal.Application.Exceptions;
using AluguelIdeal.Application.Interactors.Users.Queries;
using AluguelIdeal.Application.Repositories;
using AluguelIdeal.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace AluguelIdeal.Application.Interactors.Users.Handlers
{
    public class GetUserByIdInteractor : IRequestHandler<GetUserByIdQuery, InsensitiveUserDto>
    {
        private readonly IUserRepository userRepository;
        public GetUserByIdInteractor(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<InsensitiveUserDto> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            User user = await userRepository.GetByIdAsync(request.Id, cancellationToken);

            if (user == null || user.DeletedAt != null)
                throw new AggregateNotFoundException();

            return new InsensitiveUserDto(user);
        }
    }
}
