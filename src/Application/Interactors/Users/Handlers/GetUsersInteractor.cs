using AluguelIdeal.Application.Dtos.Users;
using AluguelIdeal.Application.Interactors.Common;
using AluguelIdeal.Application.Interactors.Users.Queries;
using AluguelIdeal.Application.Repositories;
using AluguelIdeal.Domain.Entities;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AluguelIdeal.Application.Interactors.Users.Handlers
{
    public class GetUsersInteractor : IRequestHandler<GetUsersQuery, QueryResult<InsensitiveUserDto>>
    {
        private readonly IUserRepository userRepository;
        public GetUsersInteractor(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<QueryResult<InsensitiveUserDto>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<User> users = await userRepository.ReadAsync(cancellationToken);

            return new QueryResult<InsensitiveUserDto>()
            {
                Items = users.Select(u => new InsensitiveUserDto(u)),
            };
        }
    }
}
