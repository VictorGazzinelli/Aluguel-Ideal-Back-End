using AluguelIdeal.Application.Interactors.Common;
using MediatR;
using System;

namespace AluguelIdeal.Application.Interactors.Residences.Commands
{
    public class CreateResidenceCommand : IRequest<IdResult>
    {
        public Guid Id { get; set; }
        public Guid DistrictId { get; set; }
        public string Street { get; set; }
        public int Bedrooms { get; set; }
        public int Bathrooms { get; set; }
        public double Area { get; set; }
        public double Rent { get; set; }
        public double Tax { get; set; }
        public string Description { get; set; }
    }
}
