using AluguelIdeal.Application.Repositories;
using AluguelIdeal.Domain.Entities;
using AluguelIdeal.Infrastructure.Database.Access;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AluguelIdeal.Infrastructure.Database.Repositories
{
    public class DistrictRepository : Repository, IDistrictRepository
    {
        private static readonly string SELECT = @"
            SELECT *
            FROM district
        ";

        public DistrictRepository(IDatabaseConnectionFactory databaseConnectionFactory) : base(databaseConnectionFactory)
        {}

        public async Task<IEnumerable<District>> ReadAsync(CancellationToken cancellationToken = default)
        {
            return await ExecuteQueryAsync<District>(SELECT, cancellationToken: cancellationToken);
        }
    }
}
