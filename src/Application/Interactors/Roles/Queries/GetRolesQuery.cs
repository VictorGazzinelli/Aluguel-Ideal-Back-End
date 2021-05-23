using AluguelIdeal.Application.Dtos.Roles;
using AluguelIdeal.Application.Interactors.Common;
using MediatR;

namespace AluguelIdeal.Application.Interactors.Roles.Queries
{
    public class GetRolesQuery : IRequest<QueryResult<RoleDto>>
    {
    }
}
