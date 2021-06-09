using Dapper;
using System;
using System.Collections.Generic;
using System.Data;

namespace AluguelIdeal.Infrastructure.Database.Other
{
    public class DbAccessHelper : IDisposable
    {
        protected readonly string nomeConexao;

        public DbAccessHelper(string nomeConexao)
        {
            this.nomeConexao = nomeConexao;
        }

        public void Dispose()
        {
        }

        public int Execute(string sql, object parametro) =>
            this.Execute(sql, parametro, null, null, null);

        public int Execute(string sql, object parametro, IDbTransaction transaction, int? commandTimeout, CommandType? commandType)
        {
            using (IDbConnection connection = ConnectionFactory.Instance.GetIDbConnection(nomeConexao))
            {
                return SqlMapper.Execute(connection, sql, parametro, transaction, commandTimeout, commandType);
            }
        }
    }
}
