using AluguelIdeal.Application.Dtos.Cities;
using AluguelIdeal.Application.Interactors.Common;
using MediatR;

namespace AluguelIdeal.Application.Interactors.Cities.Queries
{
    public class GetCitiesQuery : IRequest<QueryResult<CityDto>>
    {
    }
}
