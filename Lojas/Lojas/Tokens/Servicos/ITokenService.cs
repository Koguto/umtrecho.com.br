using Modelos;

namespace Tokens.Servicos
{
    public interface ITokenService
    {
        string GerarJWT(PayloadUsuario payload);
    }
}