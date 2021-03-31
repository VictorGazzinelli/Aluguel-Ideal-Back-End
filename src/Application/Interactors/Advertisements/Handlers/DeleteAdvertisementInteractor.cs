using AluguelIdeal.Application.Interactors.Advertisements.Requests;
using AluguelIdeal.Application.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace AluguelIdeal.Application.Interactors.Advertisements.Handlers
{
    public class DeleteAdvertisementInteractor : IRequestHandler<DeleteAdvertisementRequest>
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
