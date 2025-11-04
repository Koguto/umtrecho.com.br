using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Modelos
{
    public partial class FormaPagamento
    {
        public int Id { get; set; }
        public int PerfilId { get; set; }
        public string NumeroCartao { get; set; }
        public string NomeTitular { get; set; }
        public string Validade { get; set; }
        public string Cvv { get; set; }

        public virtual Perfil Perfil { get; set; }
    }
}
