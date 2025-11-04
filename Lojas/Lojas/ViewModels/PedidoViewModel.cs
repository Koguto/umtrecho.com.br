using System.Collections.Generic;
namespace ViewModels
{
    public class PedidoViewModel
    {
        public int PedidoId { get; set; }
        public List<ProdutoViewModel> Produtos { get; set; }
        public decimal ValorTotal { get; set; }
        public string EmailCliente { get; set; }

    }

}
