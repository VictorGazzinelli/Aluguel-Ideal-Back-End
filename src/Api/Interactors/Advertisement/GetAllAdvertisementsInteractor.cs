using AluguelIdeal.Api.Dto;
using AluguelIdeal.Api.Gateways.Interfaces;
using AluguelIdeal.Api.Models.Advertisement;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AluguelIdeal.Api.Interactors.Advertisement
{
    public class GetAllAdvertisementsInteractor : IRequestHandler<GetAllAdvertisementsRequest, GetAllAdvertisementsResponse>
    {
        private readonly IAdvertisementGateway advertisementGateway;

        public GetAllAdvertisementsInteractor(IAdvertisementGateway advertisementGateway) =>
            this.advertisementGateway = advertisementGateway;

        public async Task<GetAllAdvertisementsResponse> Handle (GetAllAdvertisementsRequest request, CancellationToken cancellationToken) =>
            new GetAllAdvertisementsResponse()
            {
                Advertisements = (await advertisementGateway.GetAllAsync(cancellationToken))
                    .Select(advertisement => new AdvertisementDto(advertisement))
                    .ToList()
            };
    }
}
