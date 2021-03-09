using AluguelIdeal.Api.Entities;
using AluguelIdeal.Api.Gateways.Interfaces;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AluguelIdeal.Api.Gateways
{
    public class ClientGateway : IClientGateway
    {
        public Task<IEnumerable<Client>> GetByEmailAsync(string email, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<Client>> GetByNameAsync(string name, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
