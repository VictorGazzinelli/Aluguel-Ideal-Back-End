using Dapper;
using System.Data;

namespace AluguelIdeal.Infrastructure.Database.Other
{
    public class DbAccessHelper
    {
        protected readonly string nomeConexao;
        protected readonly IConnectionFactory connectionFactory;

        public DbAccessHelper(string nomeConexao, IConnectionFactory connectionFactory)
        {
            this.nomeConexao = nomeConexao;
            this.connectionFactory = connectionFactory;
        }

        public int Execute(string sql, object parametro) =>
            this.Execute(sql, parametro, null, null, null);

        public int Execute(string sql, object parametro, IDbTransaction transaction, int? commandTimeout, CommandType? commandType)
        {
            using (IDbConnection connection = connectionFactory.GetIDbConnection(nomeConexao))
            {
                return SqlMapper.Execute(connection, sql, parametro, transaction, commandTimeout, commandType);
            }
        }
    }
}
