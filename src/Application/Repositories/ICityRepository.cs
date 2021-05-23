using AluguelIdeal.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AluguelIdeal.Application.Repositories
{
    public interface ICityRepository
    {
        Task<IEnumerable<City>> ReadAsync(CancellationToken cancellationToken = default);
        Task<City> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
