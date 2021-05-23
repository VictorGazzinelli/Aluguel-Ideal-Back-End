using System;

namespace AluguelIdeal.Domain.Entities
{
    public class Image
    {
        public Guid Id { get; set; }
        public Guid ResidenceId { get; set; }
        public string Path { get; set; }
    }
}
