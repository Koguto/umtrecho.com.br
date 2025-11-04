using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Modelos
{
    public partial class Fornecedore
    {
        public Fornecedore()
        {
            Produtos = new HashSet<Produto>();
        }

        public int FornecedorId { get; set; }
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }

        public virtual ICollection<Produto> Produtos { get; set; }
    }
}
