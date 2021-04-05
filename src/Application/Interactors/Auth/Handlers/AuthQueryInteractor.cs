using AluguelIdeal.Application.Dtos.Auth;
using AluguelIdeal.Application.Interactors.Auth.Queries;
using AluguelIdeal.Application.Services;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace AluguelIdeal.Application.Interactors.Auth.Handlers
{
    public class AuthQueryInteractor : IRequestHandler<GetAuthQuery, AuthDto>
    {
        private readonly IAuthService authService;

        public AuthQueryInteractor(IAuthService authService)
        {
            this.authService = authService;
        }

        public Task<AuthDto> Handle(GetAuthQuery request, CancellationToken cancellationToken)
            => Task.FromResult(new AuthDto() { BearerToken = authService.CreateBearerToken() });
    }
}
