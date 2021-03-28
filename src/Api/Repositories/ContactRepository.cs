using AluguelIdeal.Api.Database.Access;
using AluguelIdeal.Api.Database.Repositories;
using AluguelIdeal.Api.Entities;
using AluguelIdeal.Api.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AluguelIdeal.Api.Repositories
{
    public class ContactRepository : Repository, IContactRepository
    {
        private static readonly string INSERT = @"
                INSERT INTO contact (id, name, email, phone)
                VALUES (DEFAULT, @Name, @Email, @Phone)
                RETURNING id;
        ";

        private static readonly string SELECT = @"
                SELECT id AS Id,
                name AS Name,
                email AS Email,
                phone AS Phone
                FROM contact
                WHERE deleted_at IS NULL
        ";

        private static readonly string SELECT_BY_ID = @"
                SELECT id AS Id,
                name AS Name,
                email AS Email,
                phone AS Phone
                FROM contact
                WHERE deleted_at IS NULL
                AND id = @Id
        ";

        private static readonly string UPDATE = @"
                UPDATE contact
                SET name = @Name,
                email = @Email,
                phone = @Phone
                WHERE id = @Id
        ";

        private static readonly string DELETE = @"
                UPDATE contact
                SET deleted_at = @DeletedAt
                WHERE id = @Id
        ";

        public ContactRepository(IDatabaseConnectionFactory databaseConnectionFactory) : base(databaseConnectionFactory)
        {

        }

        public async Task<int> CreateAsync(Contact contact, CancellationToken cancellationToken = default)
        {
            return await ExecuteScalarFunctionAsync<int>(INSERT, new { contact.Name, contact.Email, contact.Phone }, cancellationToken: cancellationToken);
        }

        public async Task<IEnumerable<Contact>> ReadAsync(CancellationToken cancellationToken = default)
        {
            return await ExecuteQueryAsync<Contact>(SELECT, cancellationToken: cancellationToken);
        }

        public async Task<Contact> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return (await ExecuteQueryAsync<Contact>(SELECT_BY_ID, new { Id = id }, cancellationToken: cancellationToken)).FirstOrDefault();
        }

        public async Task UpdateAsync(Contact contact, CancellationToken cancellationToken = default)
        {
            await ExecuteCommandAsync(UPDATE, new { contact.Id, contact.Name, contact.Email, contact.Phone }, cancellationToken: cancellationToken);
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            await ExecuteCommandAsync(DELETE, new { Id = id, DeletedAt = DateTime.Now }, cancellationToken: cancellationToken);
        }
    }
}
