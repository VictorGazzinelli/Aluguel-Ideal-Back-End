using Microsoft.Extensions.Options;
using Npgsql;
using System.Data;
using System.Data.Common;
using System.Linq;
using System;

namespace AluguelIdeal.Api.Database
{
    public class DatabaseConnectionFactory : IDatabaseConnectionFactory
    {
        private readonly DatabaseSettings databaseSettings;

        public DatabaseConnectionFactory(IOptions<DatabaseSettings> databaseSettingsOptions)
        {
            this.databaseSettings = databaseSettingsOptions.Value;
            DbProviderFactories.RegisterFactory("Npgsql", NpgsqlFactory.Instance);
        }

        public IDbConnection GetDbConnection(string databaseName)
        {
            Database requestedDatabase = FindDatabaseByName(databaseName);
            DbConnection requestedDatabaseConnection = DbProviderFactories.GetFactory(requestedDatabase.Provider).CreateConnection();
            requestedDatabaseConnection.ConnectionString = requestedDatabase.Connection;
            return requestedDatabaseConnection;
        }

        private Database FindDatabaseByName(string databaseName)
        {
            if (databaseName == null)
                throw new ArgumentNullException(nameof(databaseName));

            return databaseSettings?.Databases?.FirstOrDefault(d => string.Equals(databaseName, d.Name, StringComparison.InvariantCultureIgnoreCase)) ??
            throw new CouldNotFindDatabaseException($"Could not find the database {databaseName}");
        }
    }
}
