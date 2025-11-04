using Autenticacao.Settings;
using Helper;
using Modelos;
using System.Threading.Tasks;

namespace Tokens
{
    public interface IToken
    {

        Retorno<PayloadUsuario> GerarJWT(AppSettings _appSettings, PayloadUsuario payload);

        Retorno<PayloadUsuario> LerJwtLogin(string token);
    }
}