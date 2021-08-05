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
    public class HouseRepository : Repository, IHouseRepository
    {
        private static readonly string INSERT = @"
            INSERT INTO house (id, yard_area)
            VALUES (@Id, @YardArea)
        ";

        private static readonly string UPDATE = @"
            UPDATE house
            SET yard_area = @YardArea
            WHERE id = @Id
        ";

        private static readonly string SELECT = @"
            SELECT *
            FROM house h
            INNER JOIN residence r ON r.id = h.id
            WHERE r.deleted_at IS NULL
        ";

        private static readonly string SELECT_BY_ID = @"
            SELECT *
            FROM house h
            INNER JOIN residence r ON r.id = h.id
            WHERE h.id = @Id
        ";

        public HouseRepository(IDatabaseConnectionFactory databaseConnectionFactory) : base(databaseConnectionFactory)
        { }

        public async Task CreateAsync(House house, CancellationToken cancellationToken = default)
        {
            await ExecuteCommandAsync(INSERT, house, cancellationToken: cancellationToken);
        }

        public async Task<House> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return (await ExecuteQueryAsync<House>(SELECT_BY_ID, new { Id = id }, cancellationToken: cancellationToken)).FirstOrDefault();
        }

        public async Task<IEnumerable<House>> ReadAsync(CancellationToken cancellationToken = default)
        {
            return await ExecuteQueryAsync<House>(SELECT, cancellationToken: cancellationToken);
        }

        public async Task UpdateAsync(House house, CancellationToken cancellationToken)
        {
            await ExecuteCommandAsync(UPDATE, house, cancellationToken: cancellationToken);
        }
    }
}
