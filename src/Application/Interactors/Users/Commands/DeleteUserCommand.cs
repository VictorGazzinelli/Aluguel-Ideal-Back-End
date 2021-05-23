using MediatR;
using System;

namespace AluguelIdeal.Application.Interactors.Users.Commands
{
    public class DeleteUserCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}
