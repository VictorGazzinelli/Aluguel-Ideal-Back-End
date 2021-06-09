using System;

namespace AluguelIdeal.Domain.Entities
{
    public class Role
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public object AsTableRow() =>
            new { id = Id, name = Name };
    }
}
