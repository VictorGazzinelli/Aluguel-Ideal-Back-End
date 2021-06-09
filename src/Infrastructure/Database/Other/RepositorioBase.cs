namespace AluguelIdeal.Infrastructure.Database.Other
{
    public class RepositorioBase<TEntidade> : AdoHelper where TEntidade : class
    {
        public RepositorioBase(string nomeConexao, IConnectionFactory connectionFactory) : base(nomeConexao, connectionFactory)
        {
        }
    }
}
