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

        public ResidenceDto()
        {
            
        }

        public override bool Equals(object obj)
        {
            return obj is ResidenceDto dto &&
                   Id.Equals(dto.Id) &&
                   DistrictId.Equals(dto.DistrictId) &&
                   Street == dto.Street &&
                   Bedrooms == dto.Bedrooms &&
                   Bathrooms == dto.Bathrooms &&
                   Area == dto.Area &&
                   Rent == dto.Rent &&
                   Tax == dto.Tax &&
                   Description == dto.Description;
        }

        public override int GetHashCode()
        {
            HashCode hash = new HashCode();
            hash.Add(Id);
            hash.Add(DistrictId);
            hash.Add(Street);
            hash.Add(Bedrooms);
            hash.Add(Bathrooms);
            hash.Add(Area);
            hash.Add(Rent);
            hash.Add(Tax);
            hash.Add(Description);
            return hash.ToHashCode();
        }
    }
}
