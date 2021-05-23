using System;

namespace AluguelIdeal.Domain.Entities
{
    public class Landlord
    {
        public Guid UserId { get; set; }
        public Guid ResidenceId { get; set; }
    }
}
