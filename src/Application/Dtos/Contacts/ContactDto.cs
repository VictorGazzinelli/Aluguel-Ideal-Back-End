using System;
using ContactEntity = AluguelIdeal.Domain.Entities.Contact;

namespace AluguelIdeal.Application.Dtos.Contacts
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

        public ContactDto(ContactEntity contact)
        {
            Id = contact.Id;
            Name = contact.Name;
            Email = contact.Email;
            Phone = contact.Phone;
        }

        public override bool Equals(object obj) =>
            obj is ContactDto dto &&
            Id == dto.Id &&
            Equals(Name, dto.Name) &&
            Equals(Email, dto.Email) &&
            Equals(Phone, dto.Phone);

        public bool EqualsIgnoreId(object obj) =>
            obj is ContactDto dto &&
            Equals(Name, dto.Name) &&
            Equals(Email, dto.Email) &&
            Equals(Phone, dto.Phone);

        public override int GetHashCode() =>
            HashCode.Combine(Id, Name, Email, Phone);
    }
}
