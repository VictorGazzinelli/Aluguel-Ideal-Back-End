using AluguelIdeal.Application.Dto.Advertisements;
using AluguelIdeal.Application.Interactors.Advertisements.Requests;
using AluguelIdeal.Application.Interactors.Advertisements.Responses;
using AluguelIdeal.Application.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using AdvertisementEntity = AluguelIdeal.Domain.Entities.Advertisement;

namespace AluguelIdeal.Application.Interactors.Advertisements.Handlers
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
