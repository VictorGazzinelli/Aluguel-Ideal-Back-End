﻿using AluguelIdeal.Api.Dto.Advertisement;
using AluguelIdeal.Api.Interactors.Advertisement.Requests;
using AluguelIdeal.Api.Interactors.Advertisement.Responses;
using AluguelIdeal.Api.Repositories.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using AdvertisementEntity = AluguelIdeal.Api.Entities.Advertisement;

namespace AluguelIdeal.Api.Interactors.Advertisement
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
                Advertisement = new AdvertisementDto(advertisement)
            };
        }
    }
}
