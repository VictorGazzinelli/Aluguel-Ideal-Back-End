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
    public class UserRepository : Repository, IUserRepository
    {
        private static readonly string INSERT = @"
            INSERT INTO ""user"" (Id, Name, Email, Phone, Password, deleted_at)
            VALUES (@Id, @Name, @Email, @Phone, @Password, @DeletedAt)
        ";

        private static readonly string SELECT = @"
            SELECT *
            FROM ""user""
            WHERE deleted_at IS NULL
        ";

        private static readonly string SELECT_BY_ID = @"
            SELECT *
            FROM ""user""
            WHERE id = @Id
        ";

        private static readonly string UPDATE = @"
            UPDATE ""user""
            SET name = @Name,
            email = @Email,
            phone = @Phone,
            password = @Password
            WHERE id = @Id
            AND deleted_at IS NULL
        ";

        private static readonly string DELETE = @"
            UPDATE ""user""
            SET deleted_at = @DeletedAt
            WHERE id = @Id
        ";

        public UserRepository(IDatabaseConnectionFactory databaseConnectionFactory) : base(databaseConnectionFactory)
        {}

        public async Task CreateAsync(User user, CancellationToken cancellationToken = default)
        {
            await ExecuteCommandAsync(INSERT, user, cancellationToken: cancellationToken);
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            int numberOfLinesAffected = await ExecuteCommandAsync(DELETE, new { Id = id, DeletedAt = DateTime.UtcNow }, cancellationToken: cancellationToken);

            if (numberOfLinesAffected <= 0)
                throw new UnexpectedDatabaseBehaviourException();
        }

        public async Task<User> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return (await ExecuteQueryAsync<User>(SELECT_BY_ID, new { Id = id }, cancellationToken: cancellationToken)).FirstOrDefault();
        }

        public async Task<User> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
        {
            return (await ExecuteQueryAsync<User>(SELECT, cancellationToken: cancellationToken)).FirstOrDefault(u => u.Email.Equals(email));
        }

        public async Task<IEnumerable<User>> ReadAsync(CancellationToken cancellationToken = default)
        {
            return await ExecuteQueryAsync<User>(SELECT, cancellationToken: cancellationToken);
        }

        public async Task UpdateAsync(User user, CancellationToken cancellationToken = default)
        {
            await ExecuteCommandAsync(UPDATE, user, cancellationToken: cancellationToken);
        }
    }
}
