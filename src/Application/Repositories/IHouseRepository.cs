using AluguelIdeal.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AluguelIdeal.Application.Repositories
{
    public interface IHouseRepository
    {
        Task<IEnumerable<House>> ReadAsync(CancellationToken cancellationToken = default);
        Task<House> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task CreateAsync(House house, CancellationToken cancellationToken = default);
    }
}
