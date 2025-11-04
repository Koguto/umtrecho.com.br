using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Modelos
{
    public partial class Cadastro
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string Salt { get; set; }
        public string DataCadastro { get; set; }
    }
}
