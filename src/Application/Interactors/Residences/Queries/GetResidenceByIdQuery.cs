using AluguelIdeal.Application.Dtos.Residences;
using MediatR;
using System;

namespace AluguelIdeal.Application.Interactors.Residences.Queries
{
    public class GetResidenceByIdQuery : IRequest<ResidenceDto>
    {
        public Guid Id { get; set; }
    }
}
