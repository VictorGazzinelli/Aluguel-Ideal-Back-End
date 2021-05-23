using System.Threading;
using System.Threading.Tasks;

namespace AluguelIdeal.Application.Services
{
    public interface IHashingService
    {
        Task<string> HashAsync(string password, CancellationToken cancellationToken = default);
    }
}
