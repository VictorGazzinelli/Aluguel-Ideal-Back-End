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
                VALUES (DEFAULT, @Title)
                RETURNING id;
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
                AND advertisement.id = @Id
        ";

        private static readonly string UPDATE = @"
                UPDATE advertisement
                SET title = @Title
                WHERE id = @Id
                AND deleted_at IS NULL
        ";

        private static readonly string DELETE = @"
                UPDATE advertisement
                SET deleted_at = @DeletedAt
                WHERE id = @Id
        ";

        public AdvertisementRepository (IDatabaseConnectionFactory databaseConnectionFactory) : base(databaseConnectionFactory)
        {

        }

        public async Task<int> CreateAsync(Advertisement advertisement, CancellationToken cancellationToken = default)
        {
            return await ExecuteScalarFunctionAsync<int>(INSERT, new { advertisement.Title }, cancellationToken: cancellationToken);
        }

        public async Task<IEnumerable<Advertisement>> ReadAsync(CancellationToken cancellationToken = default)
        {
            return await ExecuteQueryAsync<Advertisement>(SELECT, cancellationToken: cancellationToken);
        }
        public async Task<Advertisement> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return (await ExecuteQueryAsync<Advertisement>(SELECT_BY_ID, new { Id = id }, cancellationToken: cancellationToken)).FirstOrDefault();
        }

        public async Task UpdateAsync(Advertisement advertisement, CancellationToken cancellationToken = default)
        {
            await ExecuteCommandAsync(UPDATE, new { advertisement.Id, advertisement.Title }, cancellationToken: cancellationToken);
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            await ExecuteCommandAsync(DELETE, new { Id = id, DeletedAt = DateTime.Now }, cancellationToken: cancellationToken);
        }

    }
}
