using AluguelIdeal.Domain.Entities;
using System;

namespace AluguelIdeal.Application.Dtos.Residences
{
    public class ResidenceDto
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

        public ResidenceDto(Residence residence)
        {
            Id = residence.Id;
            DistrictId = residence.DistrictId;
            Street = residence.Street;
            Bedrooms = residence.Bedrooms;
            Bathrooms = residence.Bathrooms;
            Area = residence.Area;
            Rent = residence.Rent;
            Tax = residence.Tax;
            Description = residence.Description;
        }
    }
}
