using System;

namespace AluguelIdeal.Domain.Entities
{
    public class Flat
    {
        public Guid Id { get; set; }
        public Guid ResidenceId { get; set; }
        public double Condominium { get; set; }
        public int Floor { get; set; }
    }
}
