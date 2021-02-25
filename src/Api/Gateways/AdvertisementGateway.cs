using AluguelIdeal.Api.Entities;
using AluguelIdeal.Api.Gateways.Interfaces;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AluguelIdeal.Api.Gateways
{
    public class AdvertisementGateway : IAdvertisementGateway
    {
        public Task<IEnumerable<Advertisement>> GetAllAsync(CancellationToken cancellationToken)
        {
            IEnumerable<Advertisement> result = new Advertisement[] {
                new Advertisement()
                {
                    Id = 1,
                    Title = "Title1",
                },
                new Advertisement()
                {
                    Id = 2,
                    Title = "Title2",
                }
            };
            return Task.FromResult(result);
        }
    }
}
