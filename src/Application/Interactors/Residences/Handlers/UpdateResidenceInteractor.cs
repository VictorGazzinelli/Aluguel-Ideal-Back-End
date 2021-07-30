using AluguelIdeal.Application.Interactors.Residences.Commands;
using AluguelIdeal.Application.Repositories;
using AluguelIdeal.Domain.Entities;
using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace AluguelIdeal.Application.Interactors.Residences.Handlers
{
    public class UpdateResidenceInteractor : IRequestHandler<UpdateResidenceCommand>
    {
        private readonly IResidenceRepository residenceRepository;
        private readonly IMapper mapper;
        public UpdateResidenceInteractor(IResidenceRepository residenceRepository, IMapper mapper)
        {
            this.residenceRepository = residenceRepository;
            this.mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateResidenceCommand request, CancellationToken cancellationToken)
        {
            Residence residence = mapper.Map<UpdateResidenceCommand, Residence>(request);

            await residenceRepository.UpdateAsync(residence, cancellationToken);

            return Unit.Value;
        }
    }
}
