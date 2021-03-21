using System;

namespace AluguelIdeal.Api.Entities
{
    public class Contact 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}
