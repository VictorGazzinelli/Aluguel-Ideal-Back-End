using AluguelIdeal.Application.Interactors.Common;
using MediatR;

namespace AluguelIdeal.Application.Interactors.Users.Commands
{
    public class CreateUserCommand : IRequest<IdResult>
    {
        public string Name { get; set; }
        public string Email { get; set; }
    }
}
