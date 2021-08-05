using AluguelIdeal.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AluguelIdeal.Application.Repositories
{
    public interface IResidenceRepository
    {
        Task CreateAsync(Residence residence, CancellationToken cancellationToken = default);
        Task<IEnumerable<Residence>> ReadAsync(CancellationToken cancellationToken = default);
        Task UpdateAsync(Residence residence, CancellationToken cancellationToken = default);
        Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
        Task<Residence> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
