using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Modelos
{
    public partial class Produto
    {
        public Produto()
        {
            MovimentacoesEstoques = new HashSet<MovimentacoesEstoque>();
        }

        public int ProdutoId { get; set; }
        public int Codigo { get; set; }
        public string Nome { get; set; }
        public int? CategoriaId { get; set; }
        public int? FornecedorId { get; set; }
        public decimal Preco { get; set; }
        public int? QuantidadeEstoque { get; set; }
        public string ImagemUrl { get; set; }
        public string LinkCompra { get; set; }

        public virtual Categoria Categoria { get; set; }
        public virtual Fornecedore Fornecedor { get; set; }
        public virtual ICollection<MovimentacoesEstoque> MovimentacoesEstoques { get; set; }
    }
}
