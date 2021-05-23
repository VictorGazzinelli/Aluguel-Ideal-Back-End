using AluguelIdeal.Domain.Entities;
using System;

namespace AluguelIdeal.Application.Dtos.Cities
{
    public class CityDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public CityDto(City city)
        {
            this.Id = city.Id;
            this.Name = city.Name;
        }
    }
}
