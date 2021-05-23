using AluguelIdeal.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace AluguelIdeal.Application.Repositories
{
    public interface IProfileRepository
    {
        Task Create(Profile profile, CancellationToken cancellationToken);
        Task Delete(Profile profile, CancellationToken cancellationToken);
    }
}
