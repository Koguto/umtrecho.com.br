using Modelos;
using System.Collections.Generic;

namespace Modelos
{
    public class PedidoModel
    {
        public int PedidoId { get; set; }
        public List<ProdutoModel> Produtos { get; set; }
        public decimal ValorTotal { get; set; }
        public string EmailCliente { get; set; }

    }

}
