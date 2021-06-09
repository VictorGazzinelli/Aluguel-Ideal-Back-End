namespace AluguelIdeal.Infrastructure.Database.Other
{
    public class AdoHelper
    {
        private readonly string nomeConexao;

        public AdoHelper(string nomeConexao)
        {
            this.nomeConexao = nomeConexao;
        }

        public DbAccessHelper ObterConexao()
        {
            return ObterConexao(nomeConexao);
        }

        public static DbAccessHelper ObterConexao(string nomeConexao)
        {
            return ConnectionFactory.Instance.GetConnection(nomeConexao);
        }

        public int Executar(string sql, object parametros)
        {
            using (DbAccessHelper conexao = ObterConexao())
            {
                return conexao.Execute(sql, parametros);
            }
        }

        public int ExecutarRetornandoId(string sql, object parametros)
        {
            using (DbAccessHelper conexao = ObterConexao())
            {
                return conexao.ExecuteRetornandoIdentity(sql, parametros);
            }
        }
    }
}
