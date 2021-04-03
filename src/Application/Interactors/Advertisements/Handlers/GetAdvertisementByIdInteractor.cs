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
    public class GetAdvertisementByIdInteractor : IRequestHandler<GetAdvertisementByIdRequest, GetAdvertisementByIdResponse>
    {
        private readonly IAdvertisementRepository advertisementRepository;
        public GetAdvertisementByIdInteractor(IAdvertisementRepository advertisementRepository)
        {
            this.advertisementRepository = advertisementRepository;
        }

        public async Task<GetAdvertisementByIdResponse> Handle(GetAdvertisementByIdRequest request, CancellationToken cancellationToken)
        {
            AdvertisementEntity advertisement = await advertisementRepository.GetByIdAsync(request.Id, cancellationToken);

            return new GetAdvertisementByIdResponse()
            {
                Advertisement = advertisement != null ?
                    new AdvertisementDto(advertisement) :
                    null
            };
        }
    }
}
