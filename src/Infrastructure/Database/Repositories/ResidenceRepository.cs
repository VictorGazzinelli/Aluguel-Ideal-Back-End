using AluguelIdeal.Application.Repositories;
using AluguelIdeal.Domain.Entities;
using AluguelIdeal.Infrastructure.Database.Access;
using AluguelIdeal.Infrastructure.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AluguelIdeal.Infrastructure.Database.Repositories
{
    public class ResidenceRepository : Repository, IResidenceRepository
    {

        private static readonly string INSERT = @"
            INSERT INTO residence (Id, DistrictId, Street, Bedrooms, Bathrooms, Area, Rent, Tax, Description, DeletedAt)
            VALUES (@Id, @DistrictId, @Street, @Bedrooms, @Bathrooms, @Area, @Rent, @Tax, @Description, @DeletedAt)
        ";

        private static readonly string SELECT = @"
            SELECT *
            FROM residence
            WHERE DeletedAt IS NULL
        ";

        private static readonly string SELECT_BY_ID = @"
            SELECT *
            FROM residence
            WHERE Id = @Id
        ";

        private static readonly string UPDATE = @"
            UPDATE residence
            SET DistrictId = @DistrictId,
            Street = @Street,
            Bedrooms = @Bedrooms,
            Bathrooms = @Bathrooms,
            Area = @Area,
            Rent = @Rent,
            Tax = @Tax,
            Description = @Description,
            WHERE Id = @Id
            AND DeletedAt IS NULL
        ";

        private static readonly string DELETE = @"
            UPDATE residence
            SET DeletedAt = @DeletedAt
            WHERE Id = @Id
        ";

        public ResidenceRepository(IDatabaseConnectionFactory databaseConnectionFactory) : base(databaseConnectionFactory)
        {}

        public async Task CreateAsync(Residence residence, CancellationToken cancellationToken = default)
        {
            await ExecuteCommandAsync(INSERT, residence, cancellationToken: cancellationToken);
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            int numberOfLinesAffected = await ExecuteCommandAsync(DELETE, new { Id = id, DeletedAt = DateTime.UtcNow }, cancellationToken: cancellationToken);

            if (numberOfLinesAffected <= 0)
                throw new UnexpectedDatabaseBehaviourException();
        }

        public async Task<Residence> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return (await ExecuteQueryAsync<Residence>(SELECT_BY_ID, new { Id = id }, cancellationToken: cancellationToken)).FirstOrDefault();
        }

        public async Task<IEnumerable<Residence>> ReadAsync(CancellationToken cancellationToken = default)
        {
            return await ExecuteQueryAsync<Residence>(SELECT, cancellationToken: cancellationToken);
        }

        public async Task UpdateAsync(Residence residence, CancellationToken cancellationToken = default)
        {
            int numberOfLinesAffected = await ExecuteCommandAsync(UPDATE, residence, cancellationToken: cancellationToken);

            if (numberOfLinesAffected <= 0)
                throw new UnexpectedDatabaseBehaviourException();
        }
    }
}
