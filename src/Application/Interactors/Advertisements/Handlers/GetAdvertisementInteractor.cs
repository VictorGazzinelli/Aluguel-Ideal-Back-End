using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AdvertisementEntity = AluguelIdeal.Domain.Entities.Advertisement;

namespace AluguelIdeal.Application.Interactors.Advertisements.Handlers
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
