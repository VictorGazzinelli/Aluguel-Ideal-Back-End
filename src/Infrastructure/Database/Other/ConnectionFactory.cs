using Npgsql;
using System;
using System.Configuration;
using System.Data;
using System.Data.Common;

namespace AluguelIdeal.Infrastructure.Database.Other
{
    public class ConnectionFactory : IDisposable
    {
        private static readonly object SYNC_ROOT = new object();
        [ThreadStatic]
        private static volatile ConnectionFactory instance;
        public static ConnectionFactory Instance
        {
            get
            {
                if (instance == null)
                {
                    object obj2 = SYNC_ROOT;
                    lock (obj2)
                    {
                        if (instance == null)
                        {
                            instance = new ConnectionFactory();
                        }
                    }
                }
                return instance;
            }
        }

        private ConnectionFactory()
        {
        }

        public void Dispose()
        {
            // Ignore
        }

        public DbAccessHelper GetConnection(string name) =>
            new DbAccessHelper(name);

        internal IDbConnection GetIDbConnection(string name)
        {
            ConnectionStringSettings configuration = new ConnectionStringSettings()
            {
                Name = "postgres",
                ConnectionString = "Host=localhost;Port=5432;Username=postgres;Password=Postgres2020!;Database=postgres",
                ProviderName = "Npgsql"

            };
            return CreateConnection(configuration);
        }

        private static IDbConnection CreateConnection(ConnectionStringSettings configuration)
        {
            DbProviderFactories.RegisterFactory("Npgsql", NpgsqlFactory.Instance);
            DbConnection connection1 = DbProviderFactories.GetFactory(configuration.ProviderName).CreateConnection();
            connection1.ConnectionString = configuration.ConnectionString;

            return connection1;
        }
    }
}
