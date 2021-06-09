namespace AluguelIdeal.Infrastructure.Database.Other
{
    public class RepositorioBase<TEntidade> : AdoHelper where TEntidade : class
    {
        public RepositorioBase(string nomeConexao) : base(nomeConexao)
        {
        }
    }
}
