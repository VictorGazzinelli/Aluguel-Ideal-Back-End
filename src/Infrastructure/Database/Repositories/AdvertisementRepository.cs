using AluguelIdeal.Application.Repositories;
using AluguelIdeal.Domain.Entities;
using AluguelIdeal.Infrastructure.Database.Access;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AluguelIdeal.Infrastructure.Database.Repositories
{
    public sealed class AdvertisementRepository : Repository, IAdvertisementRepository
    {
        private static readonly string INSERT = @"
                INSERT INTO advertisement (id, title)
                VALUES (@Id, @Title)
        ";

        private static readonly string SELECT = @"
                SELECT id AS Id,
                title AS Title
                FROM advertisement
                WHERE advertisement.deleted_at IS NULL
        ";

        private static readonly string SELECT_BY_ID = @"
                SELECT id AS Id,
                title AS Title
                FROM advertisement
                WHERE advertisement.deleted_at IS NULL
                AND advertisement.id = @id
        ";

        private static readonly string UPDATE = @"
                UPDATE advertisement
                SET title = @Title
                WHERE id = @Id
                AND deleted_at IS NULL
        ";

        private static readonly string DELETE = @"
                UPDATE advertisement
                SET deleted_at = now()
                WHERE id = @id
        ";

        public AdvertisementRepository (IDatabaseConnectionFactory databaseConnectionFactory) : base(databaseConnectionFactory)
        {

        }

        public async Task CreateAsync(Advertisement advertisement, CancellationToken cancellationToken = default)
        {
            await ExecuteCommandAsync(INSERT, advertisement, cancellationToken: cancellationToken);
        }

        public async Task<IEnumerable<Advertisement>> ReadAsync(CancellationToken cancellationToken = default)
        {
            return await ExecuteQueryAsync<Advertisement>(SELECT, cancellationToken: cancellationToken);
        }
        public async Task<Advertisement> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return (await ExecuteQueryAsync<Advertisement>(SELECT_BY_ID, new { id }, cancellationToken: cancellationToken)).FirstOrDefault();
        }

        public async Task UpdateAsync(Advertisement advertisement, CancellationToken cancellationToken = default)
        {
            await ExecuteCommandAsync(UPDATE, advertisement, cancellationToken: cancellationToken);
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            await ExecuteCommandAsync(DELETE, new { id }, cancellationToken: cancellationToken);
        }

    }
}
