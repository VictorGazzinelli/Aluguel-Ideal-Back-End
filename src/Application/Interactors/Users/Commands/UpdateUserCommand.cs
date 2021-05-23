using MediatR;
using System;

namespace AluguelIdeal.Application.Interactors.Users.Commands
{
    public class UpdateUserCommand : IRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
}
