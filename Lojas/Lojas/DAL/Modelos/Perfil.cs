using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Modelos
{
    public partial class Perfil
    {
        public Perfil()
        {
            Enderecos = new HashSet<Endereco>();
        }

        public int Id { get; set; }
        public string Cpf { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }

        public virtual ICollection<Endereco> Enderecos { get; set; }
    }
}
