using System;

namespace AluguelIdeal.Domain.Entities
{
    public class District
    {
        public Guid Id { get; set; }
        public Guid CityId { get; set; }
        public string Name { get; set; }
    }
}
