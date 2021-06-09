using Npgsql;
using System;
using System.Configuration;
using System.Data;
using System.Data.Common;

namespace AluguelIdeal.Infrastructure.Database.Other
{
    public class ConnectionFactory : IDisposable
    {
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
        private IDbConnection connectionSybase = null;
        private bool inTransaction { get; set; }
        [ThreadStatic]
        private static volatile ConnectionFactory instance;
        private static readonly object SYNC_ROOT = new object();

        private ConnectionFactory()
        {
        }

        public void Dispose()
        {
            if (this.connectionSybase != null)
            {
                if (this.connectionSybase.State != ConnectionState.Closed)
                {
                    this.connectionSybase.Close();
                }
                this.connectionSybase.Dispose();
                this.connectionSybase = null;
            }
        }

        public void Begin()
        {
            this.inTransaction = true;
        }

        public void End()
        {
            this.inTransaction = false;
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
            if (connection1 == null)
            {
                throw new Exception($"Não foi possível criar uma conexão para a connectionstring ( {configuration.Name} )");
            }
            connection1.ConnectionString = configuration.ConnectionString;
            return connection1;
        }
    }
}
