using System;

namespace AluguelIdeal.Domain.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        public DateTime? DeletedAt { get; set; }

        public bool HasRegistered() =>
            Password != null;
    }
}
