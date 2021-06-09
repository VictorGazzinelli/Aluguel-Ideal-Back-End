using AluguelIdeal.Application.Repositories;
using AluguelIdeal.Domain.Entities;
using AluguelIdeal.Infrastructure.Database.Other;
using System.Threading;
using System.Threading.Tasks;

namespace AluguelIdeal.Infrastructure.Database.Repositories
{
    public class ProfileBaseRepository : RepositorioBase<Profile>, IProfileRepository
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

        public ProfileBaseRepository(IConnectionFactory connectionFactory) : base("postgres", connectionFactory)
        {}

        public async Task CreateAsync(Profile profile, CancellationToken cancellationToken)
        {
            Executar(INSERT, profile);
        }

        public async Task DeleteAsync(Profile profile, CancellationToken cancellationToken)
        {
            //int numberOfLinesAffected = await ExecuteCommandAsync(DELETE, profile, cancellationToken: cancellationToken);

            //if (numberOfLinesAffected <= 0)
            //    throw new UnexpectedDatabaseBehaviourException();

            //return Task.CompletedTask;
        }
    }
}
