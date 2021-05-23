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
    public class RoleRepository : Repository, IRoleRepository
    {
        private static readonly string SELECT = @"
            SELECT *
            FROM role
        ";

        private static readonly string SELECT_BY_ID = @"
            SELECT *
            FROM role
            WHERE id = @Id
        ";

        private static readonly string SELECT_BY_USER_EMAIL = @"
            SELECT r.*
            FROM role r
            INNER JOIN profile p
            ON r.id = p.role_id
            INNER JOIN ""user"" u
            ON p.user_id = u.id
            WHERE u.email = @Email
        ";

        public RoleRepository(IDatabaseConnectionFactory databaseConnectionFactory) : base(databaseConnectionFactory)
        {}

        public async Task<Role> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return (await ExecuteQueryAsync<Role>(SELECT_BY_ID, new { Id = id }, cancellationToken: cancellationToken)).FirstOrDefault();
        }

        public async Task<IEnumerable<Role>> ReadAsync(CancellationToken cancellationToken = default)
        {
            return await ExecuteQueryAsync<Role>(SELECT, cancellationToken: cancellationToken);
        }

        public async Task<IEnumerable<Role>> ReadByUserEmailAsync(string userEmail, CancellationToken cancellationToken = default)
        {
            return await ExecuteQueryAsync<Role>(SELECT_BY_USER_EMAIL, new { Email = userEmail }, cancellationToken: cancellationToken);
        }
    }
}
