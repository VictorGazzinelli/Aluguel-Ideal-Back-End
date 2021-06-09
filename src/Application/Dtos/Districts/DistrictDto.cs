using AluguelIdeal.Domain.Entities;
using System;

namespace AluguelIdeal.Application.Dtos.Districts
{
    public class DistrictDto
    {
        public Guid Id { get; set; }
        public Guid CityId { get; set; }
        public string Name { get; set; }

        public DistrictDto()
        {

        }

        public DistrictDto(District district)
        {
            this.Id = district.Id;
            this.CityId = district.CityId;
            this.Name = district.Name;
        }

        public override bool Equals(object obj)
        {
            return obj is DistrictDto dto &&
                   Id.Equals(dto.Id) &&
                   CityId.Equals(dto.CityId) &&
                   Name == dto.Name;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, CityId, Name);
        }
    }
}
