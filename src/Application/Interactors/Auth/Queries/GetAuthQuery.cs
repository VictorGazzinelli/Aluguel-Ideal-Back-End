using AluguelIdeal.Application.Dtos.Auth;
using MediatR;

namespace AluguelIdeal.Application.Interactors.Auth.Queries
{
    public class GetAuthQuery : IRequest<AuthDto>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
