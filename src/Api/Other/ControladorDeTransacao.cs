using AluguelIdeal.Application.Repositories;
using AluguelIdeal.Infrastructure.Database.Other;
using System;
using System.Threading.Tasks;
using System.Transactions;

namespace AluguelIdeal.Api.Other
{
    public class ControladorDeTransacao : IControladorDeTransacao
    {
        public async Task FuncaoSemRetorno(Func<Task> funcaoParaRodar)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    await funcaoParaRodar();
                    scope.Complete();
                }
                finally
                {
                    ConnectionFactory.Instance.Dispose();
                }
            }
        }
    }
}
