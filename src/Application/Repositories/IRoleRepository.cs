using AluguelIdeal.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AluguelIdeal.Application.Repositories
{
    public interface IRoleRepository
    {
        Task<Role> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<IEnumerable<Role>> ReadAsync(CancellationToken cancellationToken = default);
        Task<IEnumerable<Role>> ReadByUserEmailAsync(string userEmail, CancellationToken cancellationToken = default);
    }
}
