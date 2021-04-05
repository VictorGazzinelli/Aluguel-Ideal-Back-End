using AluguelIdeal.Application.Dtos.Advertisements;
using AluguelIdeal.Application.Interactors.Advertisements.Commands;
using AluguelIdeal.Application.Repositories;
using AluguelIdeal.Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AluguelIdeal.Application.Interactors.Advertisements.Handlers
{
    public class CreateAdvertisementInteractor : IRequestHandler<CreateAdvertisementCommand, AdvertisementDto>
    {
        private readonly IAdvertisementRepository advertisementRepository;
        public CreateAdvertisementInteractor(IAdvertisementRepository advertisementRepository)
        {
            this.advertisementRepository = advertisementRepository;
        }

        public async Task<AdvertisementDto> Handle(CreateAdvertisementCommand request, CancellationToken cancellationToken)
        {
            Advertisement advertisement = new Advertisement()
            {
                Id = Guid.NewGuid(),
                Title = request.Title
            };

            await advertisementRepository.CreateAsync(advertisement, cancellationToken);

            return new AdvertisementDto(advertisement);
        }
    }
}
