using System;
using System.Threading.Tasks;

namespace AluguelIdeal.Application.Repositories
{
    public interface IControladorDeTransacao
    {
        Task FuncaoSemRetorno(Func<Task> funcaoParaRodar);
    }
}
