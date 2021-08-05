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
    public class FlatRepository : Repository, IFlatRepository
    {
        private static readonly string INSERT = @"
            INSERT INTO flat (id, condominium, floor)
            VALUES (@Id, @Condominium, @Floor)
        ";

        private static readonly string UPDATE = @"
            UPDATE flat
            SET condominium = @Condominium,
            floor = @Floor
            WHERE id = @Id
        ";

        private static readonly string SELECT = @"
            SELECT *
            FROM flat f
            INNER JOIN residence r ON r.id = f.id
            WHERE r.deleted_at IS NULL
        ";

        private static readonly string SELECT_BY_ID = @"
            SELECT *
            FROM flat f
            INNER JOIN residence r ON r.id = f.id
            WHERE f.id = @Id
        ";

        public FlatRepository(IDatabaseConnectionFactory databaseConnectionFactory) : base(databaseConnectionFactory)
        { }

        public async Task CreateAsync(Flat flat, CancellationToken cancellationToken = default)
        {
            await ExecuteCommandAsync(INSERT, flat, cancellationToken: cancellationToken);
        }

        public async Task<Flat> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return (await ExecuteQueryAsync<Flat>(SELECT_BY_ID, new { Id = id }, cancellationToken: cancellationToken)).FirstOrDefault();
        }

        public async Task<IEnumerable<Flat>> ReadAsync(CancellationToken cancellationToken = default)
        {
            return await ExecuteQueryAsync<Flat>(SELECT, cancellationToken: cancellationToken);
        }

        public async Task UpdateAsync(Flat flat, CancellationToken cancellationToken)
        {
            await ExecuteCommandAsync(UPDATE, flat, cancellationToken: cancellationToken);
        }
    }
}
