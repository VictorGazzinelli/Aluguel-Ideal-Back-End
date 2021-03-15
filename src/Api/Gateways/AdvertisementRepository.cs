using AluguelIdeal.Api.Database;
using AluguelIdeal.Api.Entities;
using AluguelIdeal.Api.Gateways.Interfaces;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace AluguelIdeal.Api.Gateways
{
    public sealed class AdvertisementRepository : Repository<Advertisement>, IAdvertisementRepository
    {
        private static readonly string INSERT = @"
                INSERT INTO ""Advertisement"" (id, title)
                VALUES (DEFAULT, @Title)
                RETURNING id;
        ";

        private static readonly string SELECT = @"
                SELECT id AS Id,
                title AS Title
                FROM ""Advertisement""
                WHERE deleteAt IS NULL
        ";

        private static readonly string UPDATE = @"
                UPDATE TOP(1) ""Advertisement""
                SET title = @Title,
                WHERE id = @Id
        ";

        private static readonly string DELETE = @"
                UPDATE TOP(1) ""Advertisement""
                SET deletedAt = @DeletedAt,
                WHERE id = @Id
        ";

        public AdvertisementRepository (IDatabaseConnectionFactory databaseConnectionFactory) : base(databaseConnectionFactory)
        {

        }

        public async Task<int> CreateAsync(Advertisement advertisement, CancellationToken cancellationToken = default)
        {
            return await ExecuteCommandReturningIdAsync(INSERT, new { advertisement.Title }, cancellationToken: cancellationToken);
        }

        public async Task<IEnumerable<Advertisement>> ReadAsync(CancellationToken cancellationToken = default)
        {
            return await ExecuteQueryAsync(SELECT, cancellationToken: cancellationToken);
        }

        public async Task UpdateAsync(Advertisement advertisement, CancellationToken cancellationToken = default)
        {
            if (await ExecuteCommandAsync(UPDATE, new { advertisement.Id, advertisement.Title }, cancellationToken: cancellationToken) == 0)
                throw new UnexpectedDatabaseBehaviourException($"Could not update the advertisement {advertisement.Id}");
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            if (await ExecuteCommandAsync(DELETE, new { Id = id, DeletedAt = DateTime.Now }, cancellationToken: cancellationToken) == 0)
                throw new UnexpectedDatabaseBehaviourException($"Could not delete the advertisement {id}");
        }
    }
}
