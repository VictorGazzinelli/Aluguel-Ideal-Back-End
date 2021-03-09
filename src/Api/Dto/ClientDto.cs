using AluguelIdeal.Api.Entities;
using System;

namespace AluguelIdeal.Api.Dto
{
    public class ClientDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public ClientDto()
        {

        }

        public ClientDto(Client client)
        {
            this.Id = client.ClientId;
            this.Name = client.Name;
            this.Email = client.Email;
        }

        public override bool Equals(object obj) =>
            obj is ClientDto dto &&
            Id == dto.Id &&
            Equals(Name, dto.Name)&&
            Equals(Email, dto.Email);

        public override int GetHashCode() =>
            HashCode.Combine(Id, Name, Email);
    }
}
