using AluguelIdeal.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AluguelIdeal.Application.Repositories
{
    public interface IFlatRepository
    {
        Task<IEnumerable<Flat>> ReadAsync(CancellationToken cancellationToken = default);
        Task<Flat> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task CreateAsync(Flat flat, CancellationToken cancellationToken = default);
        Task UpdateAsync(Flat flat, CancellationToken cancellationToken);
    }
}
