using System;

namespace AluguelIdeal.Domain.Entities
{
    public class City
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public object AsTableRow() =>
            new { id = this.Id, name = this.Name };
    }
}
