using AluguelIdeal.Application.Dtos.Roles;
using AluguelIdeal.Application.Interactors.Common;
using AluguelIdeal.Application.Interactors.Roles.Queries;
using AluguelIdeal.Application.Repositories;
using AluguelIdeal.Domain.Entities;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AluguelIdeal.Application.Interactors.Roles.Handlers
{
    public class GetRolesInteractor : IRequestHandler<GetRolesQuery, QueryResult<RoleDto>>
    {
        private readonly IRoleRepository RoleRepository;
        public GetRolesInteractor(IRoleRepository RoleRepository)
        {
            this.RoleRepository = RoleRepository;
        }

        public async Task<QueryResult<RoleDto>> Handle(GetRolesQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<Role> Roles = await RoleRepository.ReadAsync(cancellationToken);

            return new QueryResult<RoleDto>()
            {
                Items = Roles.Select(r => new RoleDto(r)),
            };
        }
    }
}
