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
    public class UpdateAdvertisementInteractor : IRequestHandler<UpdateAdvertisementRequest, UpdateAdvertisementResponse>
    {
        private readonly IAdvertisementRepository advertisementRepository;
        public UpdateAdvertisementInteractor(IAdvertisementRepository advertisementRepository)
        {
            this.advertisementRepository = advertisementRepository;
        }

        public async Task<UpdateAdvertisementResponse> Handle(UpdateAdvertisementRequest request, CancellationToken cancellationToken)
        {
            AdvertisementEntity advertisement = new AdvertisementEntity()
            {
                Id = request.Id,
                Title = request.Title
            };

            await advertisementRepository.UpdateAsync(advertisement, cancellationToken);

            return new UpdateAdvertisementResponse()
            {
                Advertisement = new AdvertisementDto(advertisement)
            };
        }
    }
}
