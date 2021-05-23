using AluguelIdeal.Domain.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AluguelIdeal.Application.Repositories
{
    public interface IDistrictRepository
    {
        Task<IEnumerable<District>> ReadAsync(CancellationToken cancellationToken = default);
    }
}
