#nullable disable

namespace Modelos
{
    public partial class ProdutoModel
    {
        public ProdutoModel()
        {
        }

        public int ProdutoId { get; set; }
        public int Codigo { get; set; }
        public string Nome { get; set; }
        public decimal Preco { get; set; }

    }
}
