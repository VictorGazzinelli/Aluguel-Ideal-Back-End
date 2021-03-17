using AluguelIdeal.Api.Dto;
using AluguelIdeal.Api.Gateways.Interfaces;
using AluguelIdeal.Api.Interactors.Advertisement.Request;
using AluguelIdeal.Api.Interactors.Advertisement.Response;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using AdvertisementEntity = AluguelIdeal.Api.Entities.Advertisement;

namespace AluguelIdeal.Api.Interactors.Advertisement
{
    public sealed class UpdateAdvertisementInteractor : IRequestHandler<UpdateAdvertisementRequest, UpdateAdvertisementResponse>
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

            await advertisementRepository.UpdateAsync(advertisement);

            return new UpdateAdvertisementResponse()
            {
                Advertisement = new AdvertisementDto(advertisement)
            };
        }
    }
}
