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
    public class CityRepository : Repository, ICityRepository
    {
        private static readonly string SELECT = @"
            SELECT *
            FROM city
        ";

        private static readonly string SELECT_BY_ID = @"
            SELECT *
            FROM city
            WHERE id = @Id
        ";

        public CityRepository(IDatabaseConnectionFactory databaseConnectionFactory) : base(databaseConnectionFactory)
        {}

        public async Task<IEnumerable<City>> ReadAsync(CancellationToken cancellationToken = default)
        {
            return await ExecuteQueryAsync<City>(SELECT, cancellationToken: cancellationToken);
        }

        public async Task<City> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return (await ExecuteQueryAsync<City>(SELECT_BY_ID, new { Id = id }, cancellationToken: cancellationToken)).FirstOrDefault();
        }
    }
}
