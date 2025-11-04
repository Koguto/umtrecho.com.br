using Autenticacao;
using DAL;
using System.Threading.Tasks;
using ViewModels;

namespace Cliente
{
    public class LoginService : ILoginService
    {
        private readonly ICadastroRepository _repository;

        public LoginService(ICadastroRepository repository)
        {
            _repository = repository;
        }

        // Método para adicionar um novo Login
        public async Task<bool> LogarAsync(LoginViewModel login)
        {
            var cadastro = await _repository.GetByEmailAsync(login.Email);
            if (cadastro == null) { 
                return false;
            }
            bool senhaCorreta = Criptografia.VerificarSenha(login.Senha, cadastro.PasswordHash);
            return senhaCorreta;
        }


    }
}
