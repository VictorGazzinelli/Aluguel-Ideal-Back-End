using AluguelIdeal.Application.Transactions;
using System;
using System.Threading.Tasks;
using System.Transactions;

namespace AluguelIdeal.Infrastructure.Transactions
{
    public class TransactionManager : ITransactionManager
    {
        public async Task RunInTransaction(Func<Task> function)
        {
            using TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
            await function().ConfigureAwait(false);
            scope.Complete();
        }
    }
}
