using Modelos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pagamentos
{
    public interface IPagamentoOpcaoServico
    {
        Task<List<PagamentoOpcaoModel>> Listar();

    }
}