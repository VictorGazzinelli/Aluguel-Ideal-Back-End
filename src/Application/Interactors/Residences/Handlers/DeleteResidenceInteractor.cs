using AluguelIdeal.Application.Interactors.Residences.Commands;
using AluguelIdeal.Application.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace AluguelIdeal.Application.Interactors.Residences.Handlers
{
    public class DeleteResidenceInteractor : IRequestHandler<DeleteResidenceCommand>
    {
        private readonly IResidenceRepository residenceRepository;
        public DeleteResidenceInteractor(IResidenceRepository residenceRepository)
        {
            this.residenceRepository = residenceRepository;
        }

        public async Task<Unit> Handle (DeleteResidenceCommand request, CancellationToken cancellationToken)
        {
            await residenceRepository.DeleteAsync(request.Id, cancellationToken);

            return Unit.Value;
        }
    }
}
