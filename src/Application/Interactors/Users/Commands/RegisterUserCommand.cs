using MediatR;
using System;

namespace AluguelIdeal.Application.Interactors.Users.Commands
{
    public class RegisterUserCommand : IRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
    }
}
