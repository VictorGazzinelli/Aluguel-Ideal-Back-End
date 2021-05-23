using MediatR;
using System;

namespace AluguelIdeal.Application.Interactors.Profiles.Commands
{
    public class BindProfileCommand : IRequest
    {
        public Guid UserId { get; set; }
        public Guid RoleId { get; set; }
    }
}
