using AluguelIdeal.Domain.Entities;
using System;

namespace AluguelIdeal.Application.Dtos.Cities
{
    public class CityDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public CityDto()
        {

        }

        public CityDto(City city)
        {
            this.Id = city.Id;
            this.Name = city.Name;
        }

        public override bool Equals(object obj)
        {
            return obj is CityDto dto &&
                   Id.Equals(dto.Id) &&
                   Name == dto.Name;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Name);
        }
    }
}
