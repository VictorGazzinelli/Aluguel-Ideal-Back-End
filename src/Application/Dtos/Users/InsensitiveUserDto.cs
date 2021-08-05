using AluguelIdeal.Domain.Entities;
using System;

namespace AluguelIdeal.Application.Dtos.Users
{
    public class InsensitiveUserDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public InsensitiveUserDto()
        {

        }

        public InsensitiveUserDto(User user)
        {
            this.Id = user.Id;
            this.Name = user.Name;
            this.Email = user.Email;
        }

        public override bool Equals(object obj)
        {
            return obj is InsensitiveUserDto dto &&
                   Id.Equals(dto.Id) &&
                   Name == dto.Name &&
                   Email == dto.Email;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Name, Email);
        }
    }
}
