using System.Data;

namespace AluguelIdeal.Api.Database
{
    public interface IDatabaseConnectionFactory
    {
        IDbConnection GetDbConnection(string databaseName = null);
    }
}
