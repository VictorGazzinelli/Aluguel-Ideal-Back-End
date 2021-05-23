using AluguelIdeal.Domain.Entities;
using System;

namespace AluguelIdeal.Application.Dtos.Roles
{
    public class RoleDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public RoleDto(Role role)
        {
            this.Id = role.Id;
            this.Name = role.Name;
        }
    }
}
