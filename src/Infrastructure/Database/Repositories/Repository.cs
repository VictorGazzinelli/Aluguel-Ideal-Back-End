using AluguelIdeal.Infrastructure.Database.Access;
using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace AluguelIdeal.Infrastructure.Database.Repositories
{
    public abstract class Repository
    {
        private readonly IDatabaseConnectionFactory databaseConnectionFactory;

        protected Repository(IDatabaseConnectionFactory databaseConnectionFactory)
        {
            this.databaseConnectionFactory = databaseConnectionFactory;
            DefaultTypeMap.MatchNamesWithUnderscores = true;
        }

        protected async Task<int> ExecuteCommandAsync(string command, object dynamicParameters = null, string databaseConnectionName = null, IDbTransaction transaction = null, CancellationToken cancellationToken = default)
        {
            using IDbConnection databaseConnection = databaseConnectionFactory.GetDbConnection(databaseConnectionName);
            return await SqlMapper.ExecuteAsync(databaseConnection, new CommandDefinition(command, dynamicParameters, transaction: transaction, cancellationToken: cancellationToken));
        }

        protected async Task<TScalar> ExecuteScalarFunctionAsync<TScalar>(string function, object dynamicParameters = null, string databaseConnectionName = null, IDbTransaction transaction = null, CancellationToken cancellationToken = default)
        {
            using IDbConnection databaseConnection = databaseConnectionFactory.GetDbConnection(databaseConnectionName);
            return await SqlMapper.ExecuteScalarAsync<TScalar>(databaseConnection, new CommandDefinition(function, dynamicParameters, transaction: transaction, cancellationToken: cancellationToken));
        }

        protected async Task<IEnumerable<TQueryResult>> ExecuteQueryAsync<TQueryResult>(string query, object dynamicParameters = null, string databaseConnectionName = null, IDbTransaction transaction = null, CancellationToken cancellationToken = default)
        {
            using IDbConnection databaseConnection = databaseConnectionFactory.GetDbConnection(databaseConnectionName);
            return await SqlMapper.QueryAsync<TQueryResult>(databaseConnection, new CommandDefinition(query, dynamicParameters, transaction: transaction, cancellationToken: cancellationToken));
        }
    }
}
