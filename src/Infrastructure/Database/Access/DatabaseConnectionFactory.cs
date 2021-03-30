using Microsoft.Extensions.Options;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Linq;

namespace AluguelIdeal.Infrastructure.Database.Access
{
    public class DatabaseConnectionFactory : IDatabaseConnectionFactory
    {
        private readonly List<ConnectionStringSettings> connectionStringSettingsList;

        public DatabaseConnectionFactory(IOptions<List<ConnectionStringSettings>> connectionStringSettingsListOptions)
        {
            connectionStringSettingsList = connectionStringSettingsListOptions.Value.Any() ?
                connectionStringSettingsListOptions.Value :
                throw new ArgumentException($"no connection string settings options were given", nameof(connectionStringSettingsListOptions));
            RegisterFactories();
        }

        private static void RegisterFactories()
        {
            DbProviderFactories.RegisterFactory("Npgsql", NpgsqlFactory.Instance);
        }

        public IDbConnection GetDbConnection(string name = null)
        {
            ConnectionStringSettings requestedConnectionStringSettings = SelectRequestedConnectionStringSettings(name);
            DbConnection requestedDatabaseConnection = 
                DbProviderFactories.GetFactory(requestedConnectionStringSettings.ProviderName).CreateConnection();
            requestedDatabaseConnection.ConnectionString = requestedConnectionStringSettings.ConnectionString;
            return requestedDatabaseConnection;
        }

        private ConnectionStringSettings SelectRequestedConnectionStringSettings(string name = null) =>
            name != null ? FindConnectionStringSettingsByName(name) : connectionStringSettingsList[0];

        private ConnectionStringSettings FindConnectionStringSettingsByName(string name) =>
             connectionStringSettingsList.FirstOrDefault(c => string.Equals(name, c.Name, StringComparison.InvariantCultureIgnoreCase)) ??
                throw new ArgumentException($"Could not find the connection string settings with name {name}", nameof(name));
    }
}
