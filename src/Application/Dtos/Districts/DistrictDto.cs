using AluguelIdeal.Domain.Entities;
using System;

namespace AluguelIdeal.Application.Dtos.Districts
{
    public class DistrictDto
    {
        public Guid Id { get; set; }
        public Guid CityId { get; set; }
        public string Name { get; set; }

        public DistrictDto(District district)
        {
            this.Id = district.Id;
            this.CityId = district.CityId;
            this.Name = district.Name;
        }
    }
}
