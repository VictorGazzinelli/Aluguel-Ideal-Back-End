using AluguelIdeal.Api.Entities;
using System;

namespace AluguelIdeal.Api.Dto
{
    public class ContactDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public ContactDto()
        {

        }

        public ContactDto(Contact client)
        {
            this.Id = client.Id;
            this.Name = client.Name;
            this.Email = client.Email;
            this.Phone = client.Phone;
        }

        public override bool Equals(object obj) =>
            obj is ContactDto dto &&
            Id == dto.Id &&
            Equals(Name, dto.Name) &&
            Equals(Email, dto.Email) &&
            Equals(Phone, dto.Phone);

        public override int GetHashCode() =>
            HashCode.Combine(Id, Name, Email, Phone);
    }
}
