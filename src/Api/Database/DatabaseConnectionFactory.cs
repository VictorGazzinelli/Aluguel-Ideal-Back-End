using Microsoft.Extensions.Options;
using Npgsql;
using System;
using System.Data;
using System.Data.Common;
using System.Linq;

namespace AluguelIdeal.Api.Database
{
    public class DatabaseConnectionFactory : IDatabaseConnectionFactory
    {
        private readonly DatabaseSettings databaseSettings;

        public DatabaseConnectionFactory(IOptions<DatabaseSettings> databaseSettingsOptions)
        {
            databaseSettings = databaseSettingsOptions.Value;
            RegisterFactories();
        }

        private static void RegisterFactories()
        {
            DbProviderFactories.RegisterFactory("Npgsql", NpgsqlFactory.Instance);
        }

        public IDbConnection GetDbConnection(string databaseName = null)
        {
            Database requestedDatabase = SelectDatabase(databaseName);
            DbConnection requestedDatabaseConnection = DbProviderFactories.GetFactory(requestedDatabase.Provider).CreateConnection();
            requestedDatabaseConnection.ConnectionString = requestedDatabase.Connection;
            return requestedDatabaseConnection;
        }

        private Database SelectDatabase(string databaseName = null) =>
            databaseName != null ? FindDatabaseByName(databaseName) : databaseSettings.Databases.First();

        private Database FindDatabaseByName(string databaseName)
        {
            if (databaseName == null)
                throw new ArgumentNullException(nameof(databaseName));

            return databaseSettings?.Databases?.FirstOrDefault(d => string.Equals(databaseName, d.Name, StringComparison.InvariantCultureIgnoreCase)) ??
                throw new CouldNotFindDatabaseException($"Could not find the database {databaseName}");
        }
    }
}
