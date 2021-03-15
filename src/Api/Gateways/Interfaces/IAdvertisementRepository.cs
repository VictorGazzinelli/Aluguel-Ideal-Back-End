using AluguelIdeal.Api.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AluguelIdeal.Api.Gateways.Interfaces
{
    public interface IAdvertisementRepository
    {
        Task<int> CreateAsync(Advertisement advertisement, CancellationToken cancellationToken = default);
        Task<IEnumerable<Advertisement>> ReadAsync(CancellationToken cancellationToken = default);
        Task UpdateAsync(Advertisement advertisement, CancellationToken cancellationToken = default);
        Task DeleteAsync(int id, CancellationToken cancellationToken = default);
    }
}
