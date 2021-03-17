using AluguelIdeal.Api.Database;
using AluguelIdeal.Api.Entities;
using AluguelIdeal.Api.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AluguelIdeal.Api.Repositories
{
    public class ContactRepository : Repository<Contact>, IContactRepository
    {
        private static readonly string INSERT = @"
                INSERT INTO ""Contact"" (id, name)
                VALUES (DEFAULT, @Name)
                RETURNING id;
        ";

        private static readonly string SELECT = @"
                SELECT id AS Id,
                name AS Name
                FROM ""Contact""
                WHERE deleteAt IS NULL
        ";

        private static readonly string SELECT_BY_ID = @"
                SELECT id AS Id,
                name AS Name
                FROM ""Contact""
                WHERE deleteAt IS NULL
                AND id = @Id
        ";

        private static readonly string UPDATE = @"
                UPDATE TOP(1) ""Contact""
                SET Name = @Name,
                WHERE id = @Id
        ";

        private static readonly string DELETE = @"
                UPDATE TOP(1) ""Contact""
                SET deletedAt = @DeletedAt,
                WHERE id = @Id
        ";

        public ContactRepository(IDatabaseConnectionFactory databaseConnectionFactory) : base(databaseConnectionFactory)
        {

        }

        public async Task<int> CreateAsync(Contact contact, CancellationToken cancellationToken = default)
        {
            return await ExecuteCommandReturningIdAsync(INSERT, new { contact.Name }, cancellationToken: cancellationToken);
        }


        public async Task<IEnumerable<Contact>> ReadAsync(CancellationToken cancellationToken = default)
        {
            return await ExecuteQueryAsync(SELECT, cancellationToken: cancellationToken);
        }

        public async Task<Contact> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return (await ExecuteQueryAsync(SELECT_BY_ID, new { Id = id }, cancellationToken: cancellationToken)).FirstOrDefault();
        }

        public async Task UpdateAsync(Contact contact, CancellationToken cancellationToken = default)
        {
            if (await ExecuteCommandAsync(UPDATE, new { contact.Id, contact.Name }, cancellationToken: cancellationToken) == 0)
                throw new UnexpectedDatabaseBehaviourException($"Could not update the contact {contact.Id}");
        }
        public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            if (await ExecuteCommandAsync(DELETE, new { Id = id, DeletedAt = DateTime.Now }, cancellationToken: cancellationToken) == 0)
                throw new UnexpectedDatabaseBehaviourException($"Could not delete the contact {id}");
        }
    }
}
