using AluguelIdeal.Application.Dtos.Users;
using AluguelIdeal.Application.Interactors.Common;
using MediatR;

namespace AluguelIdeal.Application.Interactors.Users.Queries
{
    public class GetUsersQuery : IRequest<QueryResult<InsensitiveUserDto>>
    {

    }
}
