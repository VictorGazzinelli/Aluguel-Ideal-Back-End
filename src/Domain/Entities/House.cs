using System;

namespace AluguelIdeal.Domain.Entities
{
    public class House
    {
        public Guid Id { get; set; }
        public Guid ResidenceId { get; set; }
        public double YardArea { get; set; }
    }
}
