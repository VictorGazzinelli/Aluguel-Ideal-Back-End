using AluguelIdeal.Application.Repositories;
using AluguelIdeal.Application.Transactions;
using AluguelIdeal.Domain.Entities;
using AluguelIdeal.Infrastructure.Database.Access;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AluguelIdeal.Infrastructure.Database.Repositories
{
    public class ResidenceRepository : Repository, IResidenceRepository
    {
        private readonly IFlatRepository flatRepository;
        private readonly IHouseRepository houseRepository;
        private readonly ITransactionManager transactionManager;

        private static readonly string INSERT = @"
            INSERT INTO residence (id, district_id, street, bedrooms, bathrooms, area, rent, tax, description, deleted_at)
            VALUES (@Id, @DistrictId, @Street, @Bedrooms, @Bathrooms, @Area, @Rent, @Tax, @Description, @DeletedAt)
        ";

        private static readonly string UPDATE = @"
            UPDATE residence
            SET district_id = @DistrictId,
            street = @Street,
            bedrooms = @Bedrooms,
            bathrooms = @Bathrooms,
            area = @Area,
            rent = @Rent,
            tax = @Tax,
            description = @Description
            WHERE id = @Id
            AND deleted_at IS NULL
        ";

        private static readonly string DELETE = @"
            UPDATE residence
            SET deleted_at = @DeletedAt
            WHERE id = @Id
        ";

        public ResidenceRepository(IDatabaseConnectionFactory databaseConnectionFactory,
            ITransactionManager transactionManager,
            IFlatRepository flatRepository, IHouseRepository houseRepository) : base(databaseConnectionFactory)
        {
            this.houseRepository = houseRepository;
            this.flatRepository = flatRepository;
            this.transactionManager = transactionManager;
        }

        public async Task CreateAsync(Residence residence, CancellationToken cancellationToken = default)
        {
            await transactionManager.RunInTransaction(async () =>
            {
                await ExecuteCommandAsync(INSERT, residence, cancellationToken: cancellationToken);
                if (residence is Flat flat)
                    await flatRepository.CreateAsync(flat, cancellationToken);
                if (residence is House house)
                    await houseRepository.CreateAsync(house, cancellationToken);
            });
        }

        public async Task UpdateAsync(Residence residence, CancellationToken cancellationToken = default)
        {
            await transactionManager.RunInTransaction(async () =>
            {
                await ExecuteCommandAsync(UPDATE, residence, cancellationToken: cancellationToken);
                if (residence is Flat flat)
                    await flatRepository.UpdateAsync(flat, cancellationToken);
                if (residence is House house)
                    await houseRepository.UpdateAsync(house, cancellationToken);
            });
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            await ExecuteCommandAsync(DELETE, new { Id = id, DeletedAt = DateTime.UtcNow }, cancellationToken: cancellationToken);
        }

        public async Task<IEnumerable<Residence>> ReadAsync(CancellationToken cancellationToken = default) =>
            new List<IEnumerable<Residence>>()
            {
                await flatRepository.ReadAsync(cancellationToken),
                await houseRepository.ReadAsync(cancellationToken),
            }.SelectMany(x => x);

        public async Task<Residence> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            Residence residence = await flatRepository.GetByIdAsync(id, cancellationToken);
            if (residence == null)
                residence = await houseRepository.GetByIdAsync(id, cancellationToken);
            return residence;
        }
    }
}
