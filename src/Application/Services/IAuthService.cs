using System.Threading;
using System.Threading.Tasks;

namespace AluguelIdeal.Application.Services
{
    public interface IAuthService 
    {
        Task<(string bearerToken, int expiresIn)> CreateBearerTokenAsync(string userEmail, CancellationToken cancellationToken = default);
    }
}
