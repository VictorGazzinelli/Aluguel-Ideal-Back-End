using AluguelIdeal.Application.Dtos.Cities;
using AluguelIdeal.Application.Interactors.Cities.Queries;
using AluguelIdeal.Application.Interactors.Common;
using AluguelIdeal.Application.Repositories;
using AluguelIdeal.Domain.Entities;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AluguelIdeal.Application.Interactors.Cities.Handlers
{
    public class GetCitiesInteractor : IRequestHandler<GetCitiesQuery, QueryResult<CityDto>>
    {
        private readonly ICityRepository cityRepository;
        public GetCitiesInteractor(ICityRepository cityRepository)
        {
            this.cityRepository = cityRepository;
        }

        public async Task<QueryResult<CityDto>> Handle(GetCitiesQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<City> cities = await cityRepository.ReadAsync(cancellationToken);

            return new QueryResult<CityDto>()
            {
                Items = cities.Select(r => new CityDto(r)),
            };
        }
    }
}
