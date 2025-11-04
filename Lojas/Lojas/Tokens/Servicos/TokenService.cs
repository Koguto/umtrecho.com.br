using Autenticacao.Settings;
using Microsoft.Extensions.Options;
using Modelos;

namespace Tokens.Servicos
{
    public class TokenService : ITokenService
    {
        private readonly IToken _token;
        private readonly AppSettings _appsettings;

        public TokenService(IOptions<AppSettings> appsettings, IToken token)
        {
            this._token = token;
            this._appsettings = appsettings.Value;
        }

        public string GerarJWT(PayloadUsuario payload)
        {
            var retorno = _token.GerarJWT(_appsettings, payload);
            var jwt = retorno.Propriedades.Token;
            return jwt;

        }




    }
}
