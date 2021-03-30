using System;

namespace AluguelIdeal.Domain.Entities
{
    public class Contact 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime? DeletedAt { get; set; }

        public override string ToString() =>
            $"Id: {Id}, Name: \"{Name}\", Email: \"{Email}\", Phone: \"{Phone}\", DeletedAt: {DeletedAt?.ToString() ?? "null"}";

    }
}
