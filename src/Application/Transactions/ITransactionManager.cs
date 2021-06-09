using System;
using System.Threading.Tasks;

namespace AluguelIdeal.Application.Transactions
{
    public interface ITransactionManager
    {
        Task RunInTransaction(Func<Task> function);
    }
}
