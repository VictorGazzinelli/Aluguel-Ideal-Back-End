using AluguelIdeal.Application.Dtos.Residences;
using AluguelIdeal.Application.Exceptions;
using AluguelIdeal.Application.Interactors.Residences.Queries;
using AluguelIdeal.Application.Repositories;
using AluguelIdeal.Domain.Entities;
using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace AluguelIdeal.Application.Interactors.Residences.Handlers
{
    public class GetResidenceByIdInteractor : IRequestHandler<GetResidenceByIdQuery, ResidenceDto>
    {
        private readonly IResidenceRepository residenceRepository;
        private readonly IMapper mapper;
        public GetResidenceByIdInteractor(IResidenceRepository residenceRepository, IMapper mapper)
        {
            this.residenceRepository = residenceRepository;
            this.mapper = mapper;
        }

        public async Task<ResidenceDto> Handle(GetResidenceByIdQuery request, CancellationToken cancellationToken)
        {
            Residence residence = await residenceRepository.GetByIdAsync(request.Id,cancellationToken);

            if (residence == null || residence.DeletedAt != null)
                throw new AggregateNotFoundException();

            return mapper.Map<ResidenceDto>(residence);
        }
    }
}
