using AluguelIdeal.Application.Dtos.Residences;
using AluguelIdeal.Application.Interactors.Common;
using MediatR;
using System;

namespace AluguelIdeal.Application.Interactors.Residences.Queries
{
    public class GetResidencesQuery : IRequest<QueryResult<ResidenceDto>>
    {
        public Guid? CityId { get; set; }
        public Guid? DistrictId { get; set; }
        public int? MaxPrice { get; set; }
        public int? MinBedrooms { get; set; }
    }
}
