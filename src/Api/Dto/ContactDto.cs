using AluguelIdeal.Api.Entities;
using System;

namespace AluguelIdeal.Api.Dto
{
    public class ContactDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ContactDto()
        {

        }

        public ContactDto(Contact client)
        {
            this.Id = client.Id;
            this.Name = client.Name;
        }

        public override bool Equals(object obj) =>
            obj is ContactDto dto &&
            Id == dto.Id &&
            Equals(Name, dto.Name);

        public override int GetHashCode() =>
            HashCode.Combine(Id, Name);
    }
}
