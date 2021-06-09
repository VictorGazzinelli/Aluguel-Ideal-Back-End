using AluguelIdeal.Application.Dtos.Residences;
using AluguelIdeal.Application.Exceptions;
using AluguelIdeal.Application.Interactors.Residences.Queries;
using AluguelIdeal.Application.Repositories;
using AluguelIdeal.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace AluguelIdeal.Application.Interactors.Residences.Handlers
{
    public class GetResidenceByIdInteractor : IRequestHandler<GetResidenceByIdQuery, ResidenceDto>
    {
        private readonly IResidenceRepository residenceRepository;
        public GetResidenceByIdInteractor(IResidenceRepository residenceRepository)
        {
            this.residenceRepository = residenceRepository;
        }

        public async Task<ResidenceDto> Handle(GetResidenceByIdQuery request, CancellationToken cancellationToken)
        {
            Residence residence = await residenceRepository.GetByIdAsync(request.Id,cancellationToken);

            if (residence == null || residence.DeletedAt != null)
                throw new AggregateNotFoundException();

            return new ResidenceDto(residence);
        }
    }
}
