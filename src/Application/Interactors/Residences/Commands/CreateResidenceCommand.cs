using AluguelIdeal.Application.Enums;
using AluguelIdeal.Application.Interactors.Common;
using MediatR;
using System;

namespace AluguelIdeal.Application.Interactors.Residences.Commands
{
    public class CreateResidenceCommand : IRequest<IdResult>
    {
        public ResidenceType ResidenceType { get; set; }
        public Guid DistrictId { get; set; }
        public string Street { get; set; }
        public int Bedrooms { get; set; }
        public int Bathrooms { get; set; }
        public double Area { get; set; }
        public double Rent { get; set; }
        public double Tax { get; set; }
        public string Description { get; set; }
        public double Condominium { get; set; }
        public int Floor { get; set; }
        public double YardArea { get; set; }

        public CreateResidenceCommand()
        {

        }
    }
}
