using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Modelos
{
    public partial class MovimentacoesEstoque
    {
        public int MovimentacaoId { get; set; }
        public int? ProdutoId { get; set; }
        public string TipoMovimentacao { get; set; }
        public int Quantidade { get; set; }
        public DateTime? DataMovimentacao { get; set; }

        public virtual Produto Produto { get; set; }
    }
}
