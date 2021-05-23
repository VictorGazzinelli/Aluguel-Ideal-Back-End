using AluguelIdeal.Application.Dtos.Residences;
using AluguelIdeal.Application.Interactors.Common;
using AluguelIdeal.Application.Interactors.Residences.Queries;
using AluguelIdeal.Application.Repositories;
using AluguelIdeal.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AluguelIdeal.Application.Interactors.Residences.Handlers
{
    public class GetResidencesInteractor : IRequestHandler<GetResidencesQuery, QueryResult<ResidenceDto>>
    {
        private readonly IDistrictRepository districtRepository;
        private readonly IResidenceRepository residenceRepository;
        public GetResidencesInteractor(IResidenceRepository residenceRepository, IDistrictRepository districtRepository)
        {
            this.districtRepository = districtRepository;
            this.residenceRepository = residenceRepository;
        }

        public async Task<QueryResult<ResidenceDto>> Handle(GetResidencesQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<Residence> residences = await residenceRepository.ReadAsync(cancellationToken);

            if(request.CityId != null)
            {
                IEnumerable<Guid> districtIdsInCity = (await districtRepository.ReadAsync(cancellationToken))
                    .Where(d => d.CityId == request.CityId)
                    .Select(d => d.Id);
                residences = residences.Where(r => districtIdsInCity.Any(id => r.DistrictId == id));
            }
                
            if (request.DistrictId != null)
                residences = residences.Where(r => r.DistrictId == request.DistrictId);

            if (request.MaxPrice != null)
                residences = residences.Where(r => r.GetFinalPrice() <= request.MaxPrice);

            if (request.MinBedrooms != null)
                residences = residences.Where(r => r.Bedrooms >= request.MinBedrooms);

            return new QueryResult<ResidenceDto>()
            {
                Items = residences.Select(r => new ResidenceDto(r)),
            };
        }
    }
}
