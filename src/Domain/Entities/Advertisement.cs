using System;

namespace AluguelIdeal.Domain.Entities
{
    public class Advertisement 
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime? DeletedAt { get; set; }

        public override string ToString() =>
            $"Id: {Id}, Title: \"{Title}\", DeletedAt: {DeletedAt?.ToString() ?? "null"}";
    }
}
