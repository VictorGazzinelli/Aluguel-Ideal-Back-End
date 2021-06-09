using Microsoft.Extensions.Options;
using Npgsql;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;

namespace AluguelIdeal.Infrastructure.Database.Other
{
    public class ConnectionFactory : IConnectionFactory
    {
        private readonly List<ConnectionStringSettings> connectionStringSettingsList;

        public ConnectionFactory(IOptions<List<ConnectionStringSettings>> connectionStringSettingsListOptions)
        {

        }

        public IDbConnection GetIDbConnection(string name)
        {
            ConnectionStringSettings configuration = new ConnectionStringSettings()
            {
                Name = "postgres",
                ConnectionString = "Host=localhost;Port=5432;Username=postgres;Password=Postgres2020!;Database=postgres",
                ProviderName = "Npgsql"

            };
            return CreateConnection(configuration);
        }

        private IDbConnection CreateConnection(ConnectionStringSettings configuration)
        {
            DbProviderFactories.RegisterFactory("Npgsql", NpgsqlFactory.Instance);
            DbConnection connection1 = DbProviderFactories.GetFactory(configuration.ProviderName).CreateConnection();
            connection1.ConnectionString = configuration.ConnectionString;

            return connection1;
        }
    }
}
