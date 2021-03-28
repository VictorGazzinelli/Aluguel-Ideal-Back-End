using System.Data;

namespace AluguelIdeal.Api.Database.Access
{
    public interface IDatabaseConnectionFactory
    {
        IDbConnection GetDbConnection(string name = null);
    }
}
