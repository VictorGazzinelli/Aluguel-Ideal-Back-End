using AluguelIdeal.Application.Dtos.Districts;
using AluguelIdeal.Application.Interactors.Common;
using AluguelIdeal.Application.Interactors.Districts.Queries;
using AluguelIdeal.Application.Repositories;
using AluguelIdeal.Domain.Entities;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AluguelIdeal.Application.Interactors.Districts.Handlers
{
    public class GetDistrictsInteractor : IRequestHandler<GetDistrictsQuery, QueryResult<DistrictDto>>
    {
        private readonly IDistrictRepository districtRepository;
        public GetDistrictsInteractor(IDistrictRepository districtRepository)
        {
            this.districtRepository = districtRepository;
        }

        public async Task<QueryResult<DistrictDto>> Handle(GetDistrictsQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<District> districts = await districtRepository.ReadAsync(cancellationToken);

            if (request.CityId != null)
                districts = districts.Where(d => d.CityId == request.CityId);

            return new QueryResult<DistrictDto>()
            {
                Items = districts.Select(r => new DistrictDto(r)),
            };
        }
    }
}
