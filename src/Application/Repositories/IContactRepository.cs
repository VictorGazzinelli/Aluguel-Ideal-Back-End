using AluguelIdeal.Domain.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AluguelIdeal.Application.Repositories
{
    public interface IContactRepository
    {
        Task<int> CreateAsync(Contact contact, CancellationToken cancellationToken = default);
        Task<IEnumerable<Contact>> ReadAsync(CancellationToken cancellationToken = default);
        Task<Contact> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task UpdateAsync(Contact contact, CancellationToken cancellationToken = default);
        Task DeleteAsync(int id, CancellationToken cancellationToken = default);
    }
}
