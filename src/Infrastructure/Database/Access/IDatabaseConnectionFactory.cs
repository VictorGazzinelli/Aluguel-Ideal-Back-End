using System.Data;

namespace AluguelIdeal.Infrastructure.Database.Access
{
    public interface IDatabaseConnectionFactory
    {
        IDbConnection GetDbConnection(string name = null);
    }
}
