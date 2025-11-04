#nullable disable

namespace ViewModels
{
    public class ProdutoViewModel
    {
        public int ProdutoId { get; set; }
        public int Codigo { get; set; }
        public string Nome { get; set; }
        public decimal Preco { get; set; }
        public int QuantidadeEstoque { get; set; }
        public string ImagemUrl { get; set; }
        public string LinkCompra { get; set; }

    }

}
