﻿using AluguelIdeal.Api.Repositories.Interfaces;
using AluguelIdeal.Api.Interactors.Advertisement.Request;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace AluguelIdeal.Api.Interactors.Advertisement
{
    public sealed class DeleteAdvertisementInteractor : IRequestHandler<DeleteAdvertisementRequest>
    {
        private readonly IAdvertisementRepository advertisementRepository;
        public DeleteAdvertisementInteractor(IAdvertisementRepository advertisementRepository)
        {
            this.advertisementRepository = advertisementRepository;
        }
        public async Task<Unit> Handle(DeleteAdvertisementRequest request, CancellationToken cancellationToken)
        {
            await advertisementRepository.DeleteAsync(request.Id, cancellationToken);

            return Unit.Value;
        }
    }
}
