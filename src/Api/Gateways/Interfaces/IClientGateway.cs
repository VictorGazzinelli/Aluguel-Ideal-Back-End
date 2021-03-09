using AluguelIdeal.Api.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AluguelIdeal.Api.Gateways.Interfaces
{
    public interface IClientGateway
    {
        Task<IEnumerable<Client>> GetByNameAsync(string name, CancellationToken cancellationToken);
        Task<IEnumerable<Client>> GetByEmailAsync(string email, CancellationToken cancellationToken);
    }
}
