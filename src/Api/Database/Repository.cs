using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace AluguelIdeal.Api.Database
{
    public abstract class Repository<TEntity> where TEntity : class
    {
        private readonly IDatabaseConnectionFactory databaseConnectionFactory;

        protected Repository(IDatabaseConnectionFactory databaseConnectionFactory)
        {
            this.databaseConnectionFactory = databaseConnectionFactory;
        }

        protected async Task<int> ExecuteCommandAsync(string command, object dynamicParameters = null, string databaseName = null, CancellationToken cancellationToken = default)
        {
            using IDbConnection databaseConnection = databaseConnectionFactory.GetDbConnection(databaseName);
            return await SqlMapper.ExecuteAsync(databaseConnection, new CommandDefinition(command, dynamicParameters, cancellationToken: cancellationToken));
        }

        protected async Task<TScalar> ExecuteScalarFunctionAsync<TScalar>(string function, object dynamicParameters = null, string databaseName = null, CancellationToken cancellationToken = default)
        {
            using IDbConnection databaseConnection = databaseConnectionFactory.GetDbConnection(databaseName);
            return await SqlMapper.ExecuteScalarAsync<TScalar>(databaseConnection, new CommandDefinition(function, dynamicParameters, cancellationToken: cancellationToken));
        }

        protected async Task<IEnumerable<TEntity>> ExecuteQueryAsync(string query, object dynamicParameters = null, string databaseName = null, CancellationToken cancellationToken = default)
        {
            using IDbConnection databaseConnection = databaseConnectionFactory.GetDbConnection(databaseName);
            return await SqlMapper.QueryAsync<TEntity>(databaseConnection, new CommandDefinition(query, dynamicParameters, cancellationToken: cancellationToken));
        }
    }
}
