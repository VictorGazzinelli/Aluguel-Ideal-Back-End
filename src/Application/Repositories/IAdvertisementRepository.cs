using AluguelIdeal.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AluguelIdeal.Application.Repositories
{
    public interface IAdvertisementRepository
    {
        Task CreateAsync(Advertisement advertisement, CancellationToken cancellationToken = default);
        Task<IEnumerable<Advertisement>> ReadAsync(CancellationToken cancellationToken = default);
        Task<Advertisement> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task UpdateAsync(Advertisement advertisement, CancellationToken cancellationToken = default);
        Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
