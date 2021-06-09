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
                using (CallContext context = CallContext.Instance)
                {
                    if (context.CallerMetadata != null)
                    {
                        context.HistoryCallback(connection, sql, parametro);
                    }
                }
                return SqlMapper.Execute(connection, sql, parametro, transaction, commandTimeout, commandType);
            }
        }

        public int ExecuteRetornandoIdentity(string sql, object parametro)
        {
            using (IDbConnection connection = ConnectionFactory.Instance.GetIDbConnection(nomeConexao))
            {
                using (CallContext context = CallContext.Instance)
                {
                    if (context.CallerMetadata != null)
                    {
                        context.HistoryCallback(connection, sql, parametro);
                    }
                }
                sql = sql + " \nSELECT @@IDENTITY AS int";
                return SqlMapper.ExecuteScalar<int>(connection, sql, parametro, null, null, null);
            }
        }

        public IEnumerable<T> Query<T>(string sql, object param)
        {
            using (IDbConnection connection = ConnectionFactory.Instance.GetIDbConnection(nomeConexao))
            {
                return SqlMapper.Query<T>(connection, sql, param, null, true, null, null);
            }
        }

        public IEnumerable<T> Query<T>(string sql, object parametro, bool bufferizado = true, int? timeoutComando = new int?(), CommandType? tipoComando = new CommandType?())
        {
            using (IDbConnection connection = ConnectionFactory.Instance.GetIDbConnection(nomeConexao))
            {
                return SqlMapper.Query<T>(connection, sql, parametro, null, bufferizado, timeoutComando, tipoComando);
            }
        }
    }
}
