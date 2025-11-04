using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ViewModels
{
    // Models/PerfilViewModel.cs

    public class PerfilViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome deve ter no máximo 100 caracteres.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O email é obrigatório.")]
        [EmailAddress(ErrorMessage = "Email inválido.")]
        public string Email { get; set; }

        public EnderecoViewModel Endereco { get; set; }
        public List<EnderecoViewModel> EnderecosAdicionados { get; set; } = new List<EnderecoViewModel>();

    }

    public class EnderecoViewModel
    {
        [Required(ErrorMessage = "O logradouro é obrigatório.")]
        [Display(Name = "Logradouro")]

        public string Rua { get; set; }

        [Required(ErrorMessage = "O número é obrigatório.")]
        public string Numero { get; set; }

        public string Complemento { get; set; }

        [Required(ErrorMessage = "O bairro é obrigatório.")]
        public string Bairro { get; set; }

        [Required(ErrorMessage = "A cidade é obrigatória.")]
        public string Cidade { get; set; }

        [Required(ErrorMessage = "O estado é obrigatório.")]
        public string Estado { get; set; }

        [Required(ErrorMessage = "O CEP é obrigatório.")]
        [RegularExpression(@"^\d{5}-\d{3}$", ErrorMessage = "CEP inválido.")]
        public string CEP { get; set; }
    }


}
