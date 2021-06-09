using System.Collections.Generic;
using System.Linq;

namespace AluguelIdeal.Infrastructure.Database.Other
{
    public class RepositorioBase<TEntidade> : AdoHelper where TEntidade : class
    {
        public RepositorioBase(string nomeConexao) : base(nomeConexao)
        {
        }

        protected TEntidade Obter(string consulta, object parametros)
        {
            using (var conexao = ObterConexao())
            {
                return conexao.Query<TEntidade>(consulta, parametros).FirstOrDefault();
            }
        }
        protected T Obter<T>(string consulta, object parametros)
        {
            using (var conexao = ObterConexao())
            {
                return conexao.Query<T>(consulta, parametros).FirstOrDefault();
            }
        }

        protected IEnumerable<TEntidade> Listar(string consulta, object parametros, bool buferizado = true, int? timeoutComando = null)
        {
            using (var conexao = ObterConexao())
            {
                return conexao.Query<TEntidade>(consulta, parametros, buferizado, timeoutComando);
            }
        }

        protected IEnumerable<T> Listar<T>(string consulta, object parametros, bool buferizado = true, int? timeoutComando = null)
        {
            using (var conexao = ObterConexao())
            {
                return conexao.Query<T>(consulta, parametros, buferizado, timeoutComando);
            }
        }
    }
}
