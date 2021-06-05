using AluguelIdeal.Application.Repositories;
using AluguelIdeal.Domain.Entities;
using AluguelIdeal.Infrastructure.Database.Access;
using AluguelIdeal.Infrastructure.Exceptions;
using System.Threading;
using System.Threading.Tasks;

namespace AluguelIdeal.Infrastructure.Database.Repositories
{
    public class ProfileRepository : Repository, IProfileRepository
    {
        private static readonly string INSERT = @"
            INSERT INTO profile (user_id, role_id)
            VALUES (@UserId, @RoleId)
        ";

        private static readonly string DELETE = @"
            DELETE FROM profile
            WHERE user_id = @UserId 
            AND role_id = @RoleId
        ";

        public ProfileRepository(IDatabaseConnectionFactory databaseConnectionFactory) : base(databaseConnectionFactory)
        {}

        public async Task Create(Profile profile, CancellationToken cancellationToken)
        {
            await ExecuteCommandAsync(INSERT, profile, cancellationToken: cancellationToken);
        }

        public async Task Delete(Profile profile, CancellationToken cancellationToken)
        {
            int numberOfLinesAffected = await ExecuteCommandAsync(DELETE, profile, cancellationToken: cancellationToken);

            if (numberOfLinesAffected <= 0)
                throw new UnexpectedDatabaseBehaviourException();
        }
    }
}
