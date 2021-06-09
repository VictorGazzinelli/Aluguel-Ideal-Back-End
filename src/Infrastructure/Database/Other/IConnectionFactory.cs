using System.Data;

namespace AluguelIdeal.Infrastructure.Database.Other
{
    public interface IConnectionFactory
    {
        IDbConnection GetIDbConnection(string name);
    }
}
