using AluguelIdeal.Api.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AluguelIdeal.Api.Gateways.Interfaces
{
    public interface IAdvertisementGateway
    {
        Task<IEnumerable<Advertisement>> GetAllAsync(CancellationToken cancellationToken);
    }
}
