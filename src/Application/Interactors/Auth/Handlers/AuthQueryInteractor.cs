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

        public async Task<AuthDto> Handle(GetAuthQuery request, CancellationToken cancellationToken)
        {
            var (bearerToken, expiresIn) = await authService.CreateBearerTokenAsync(request.Email, cancellationToken);

            return new AuthDto() 
            {
                AccessToken = bearerToken,
                ExpiresIn = expiresIn
            };
        }
    }
}
