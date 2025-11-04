using Modelos;
using System.Collections.Generic;

namespace Modelos
{
    public class PerfilModel
    {
        public int PerfilId { get; set; }
        public List<ProdutoModel> Produtos { get; set; }
        public decimal ValorTotal { get; set; }
        public string EmailCliente { get; set; }

    }

}
