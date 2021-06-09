namespace AluguelIdeal.Infrastructure.Database.Other
{
    public class AdoHelper
    {
        private readonly string nomeConexao;
        private readonly IConnectionFactory connectionFactory;

        public AdoHelper(string nomeConexao, IConnectionFactory connectionFactory)
        {
            this.nomeConexao = nomeConexao;
            this.connectionFactory = connectionFactory;
        }

        public int Executar(string sql, object parametros)
        {
            DbAccessHelper conexao = new DbAccessHelper(nomeConexao, connectionFactory);
            return conexao.Execute(sql, parametros);
        }
    }
}
