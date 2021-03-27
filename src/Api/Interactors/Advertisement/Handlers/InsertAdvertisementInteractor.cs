using AluguelIdeal.Api.Dto;
using AluguelIdeal.Api.Repositories.Interfaces;
using AluguelIdeal.Api.Interactors.Advertisement.Request;
using AluguelIdeal.Api.Interactors.Advertisement.Response;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using AdvertisementEntity = AluguelIdeal.Api.Entities.Advertisement;

namespace AluguelIdeal.Api.Interactors.Advertisement
{
    public class InsertAdvertisementInteractor : IRequestHandler<InsertAdvertisementRequest, InsertAdvertisementResponse>
    {
        private readonly IAdvertisementRepository advertisementRepository;
        public InsertAdvertisementInteractor(IAdvertisementRepository advertisementRepository)
        {
            this.advertisementRepository = advertisementRepository;
        }

        public async Task<InsertAdvertisementResponse> Handle(InsertAdvertisementRequest request, CancellationToken cancellationToken)
        {
            AdvertisementEntity advertisement = new AdvertisementEntity()
            {
                Title = request.Title
            };

            advertisement.Id = await advertisementRepository.CreateAsync(advertisement, cancellationToken);

            return new InsertAdvertisementResponse()
            {
                Advertisement = new AdvertisementDto(advertisement)
            };
        }
    }
}
