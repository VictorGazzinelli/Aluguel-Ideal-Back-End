using AluguelIdeal.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace AluguelIdeal.Application.Repositories
{
    public interface IProfileRepository
    {
        Task CreateAsync(Profile profile, CancellationToken cancellationToken);
        Task DeleteAsync(Profile profile, CancellationToken cancellationToken);
    }
}
