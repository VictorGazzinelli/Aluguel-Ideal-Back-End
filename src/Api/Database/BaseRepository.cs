using Dapper;
using System.Data;
using System.Threading.Tasks;

namespace AluguelIdeal.Api.Database
{
    public class BaseRepository
    {
        private readonly IDatabaseConnectionFactory databaseConnectionFactory;
        private readonly string databaseName;

        public BaseRepository(IDatabaseConnectionFactory databaseConnectionFactory, string databaseName)
        {
            this.databaseConnectionFactory = databaseConnectionFactory;
            this.databaseName = databaseName;
        }

        protected async Task<int> ExecuteInsertCommandReturningIdAsync(string command, object dynamicParameters)
        {
            int id;
            using (IDbConnection databaseConnection = databaseConnectionFactory.GetDbConnection(databaseName))
            {
                command += " RETURNING id";
                id = await SqlMapper.ExecuteScalarAsync<int>(databaseConnection, command, dynamicParameters);
            }
            return id;
        }

        protected async Task<int> ExecuteCommandAsync(string command, object dynamicParameters)
        {
            int numberOfRowsAffected;
            using (IDbConnection databaseConnection = databaseConnectionFactory.GetDbConnection(databaseName))
            {
                numberOfRowsAffected = await SqlMapper.ExecuteAsync(databaseConnection, command, dynamicParameters);
            }
            return numberOfRowsAffected;
        }
    }
}
