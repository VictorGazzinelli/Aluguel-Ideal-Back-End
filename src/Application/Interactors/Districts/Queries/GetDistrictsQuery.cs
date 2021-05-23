using AluguelIdeal.Application.Dtos.Districts;
using AluguelIdeal.Application.Interactors.Common;
using MediatR;
using System;

namespace AluguelIdeal.Application.Interactors.Districts.Queries
{
    public class GetDistrictsQuery : IRequest<QueryResult<DistrictDto>>
    {
        public Guid? CityId { get; set; }
    }
}
