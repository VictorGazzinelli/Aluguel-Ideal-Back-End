using AluguelIdeal.Api.Dto;
using AluguelIdeal.Api.Repositories.Interfaces;
using AluguelIdeal.Api.Interactors.Advertisement.Request;
using AluguelIdeal.Api.Interactors.Advertisement.Response;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AdvertisementEntity = AluguelIdeal.Api.Entities.Advertisement;

namespace AluguelIdeal.Api.Interactors.Advertisement
{
    public class GetAdvertisementInteractor : IRequestHandler<GetAdvertisementRequest, GetAdvertisementResponse>
    {
        private readonly IAdvertisementRepository advertisementRepository;
        public GetAdvertisementInteractor(IAdvertisementRepository advertisementRepository)
        {
            this.advertisementRepository = advertisementRepository;
        }

        public async Task<GetAdvertisementResponse> Handle(GetAdvertisementRequest request, CancellationToken cancellationToken)
        {
            IEnumerable<AdvertisementEntity> advertisements = await advertisementRepository.ReadAsync(cancellationToken);

            return new GetAdvertisementResponse()
            {
                Advertisements = advertisements.Select(a => new AdvertisementDto(a)).ToList()
            };
        }
    }
}
