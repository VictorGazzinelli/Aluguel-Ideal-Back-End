using AluguelIdeal.Api.Dto.Advertisement;
using AluguelIdeal.Api.Interactors.Advertisement.Requests;
using AluguelIdeal.Api.Interactors.Advertisement.Responses;
using AluguelIdeal.Api.Repositories.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using AdvertisementEntity = AluguelIdeal.Api.Entities.Advertisement;

namespace AluguelIdeal.Api.Interactors.Advertisement
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
