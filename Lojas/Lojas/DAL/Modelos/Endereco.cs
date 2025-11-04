using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Modelos
{
    public partial class Endereco
    {
        public int Id { get; set; }
        public int PerfilId { get; set; }
        public string Rua { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public string Cep { get; set; }

        public virtual Perfil Perfil { get; set; }
    }
}
