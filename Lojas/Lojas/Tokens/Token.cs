using Autenticacao;
using Autenticacao.Settings;
using Helper;
using Helper.Utils;
using LogDefault;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Modelos;
using Newtonsoft.Json;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;



namespace Tokens
{
    public class Token :IToken
    {
        private readonly AppSettings _appSettings;

        public Token(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;


        }
        public Retorno<PayloadUsuario> GerarJWT(AppSettings _appSettings, PayloadUsuario payload)
        {
            try
            {
                var payloadUsuarioJson = JsonConvert.SerializeObject(payload);
                var payloadUsuarioCrypto = Criptografia.Criptografar(payloadUsuarioJson);

                var identityClaims = new ClaimsIdentity();
                var claimsPayloadLogin = new Claim("payload_usuario", payloadUsuarioCrypto);
                identityClaims.AddClaim(claimsPayloadLogin);


                var key = Encoding.ASCII.GetBytes(_appSettings.TokenSecret);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = identityClaims
                    ,
                    Issuer = _appSettings.Emissor
                    ,
                    Audience = _appSettings.ValidoEm
                    ,
                    Expires = DateTime.UtcNow.AddHours(Convert.ToInt32(_appSettings.ExpiracaoHoras))
                    ,
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key)
                    , SecurityAlgorithms.HmacSha256Signature)
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var jwtToken = tokenHandler.WriteToken(token);

                #region Teste de dados descriptografados
                //var testeCryptografia = Criptografia.Descriptografar(payloadAutenticacaoJsonCrypto);
                //var dadosPayload = System.Text.Json.JsonSerializer.Deserialize<Autenticacao>(testeCryptografia);
                #endregion

                payload.Token = jwtToken;

                var retorno = new Retorno<PayloadUsuario>()
                {
                    Codigo = CODIGO.Sucesso
                    ,
                    MensagemSucesso = "Sucesso ao gerar o token"
                    ,
                    Propriedades = payload
                };

                return retorno;
            }
            catch (Exception ex)
            {
                LogHelper.Instancia.Error(
                    string.Concat(LogTemplates.ERRO_NEGOCIO
                                , "Token", "GerarJWT"
                                ), ex);

                var retorno = new Retorno<PayloadUsuario>()
                {
                    Codigo = CODIGO.Erro
                 ,
                    MensagemErro = "Erro ao gerar o token: " + ex.Message
                 ,
                    Propriedades = new PayloadUsuario()
                };

                return retorno;
            }

        }


        public Retorno<PayloadUsuario> LerJwtLogin(string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var jwtToken = tokenHandler.ReadJwtToken(token);

                // Acesse as reivindicações personalizadas do token
                //var email = jwtToken.Claims.FirstOrDefault(c => c.Type == "Email")?.Value;
                //var ticket = jwtToken.Claims.FirstOrDefault(c => c.Type == "IdUser")?.Value;
                var payloadClaimCrypto = jwtToken.Claims.FirstOrDefault(c => c.Type == "payload_usuario")?.Value;
                var payloadJson = Criptografia.Descriptografar(payloadClaimCrypto);
                var dadosPayload = JsonConvert.DeserializeObject<PayloadUsuario>(payloadJson);

                var retorno = new Retorno<PayloadUsuario>()
                {
                    Codigo = CODIGO.Sucesso
                    ,
                    MensagemSucesso = "Sucesso ao ler o token"
                    ,
                    Propriedades = dadosPayload
                };

                return retorno;
            }
            catch (Exception ex)
            {
                LogHelper.Instancia.Error(
                    string.Concat(LogTemplates.ERRO_NEGOCIO
                                , "TokenBusiness", "LerJwtLogin"
                                ), ex);

                var retorno = new Retorno<PayloadUsuario>()
                {
                    Codigo = CODIGO.Erro
                 ,
                    MensagemSucesso = "Erro ao ler o token"
                 ,
                    Propriedades = new PayloadUsuario()
                };

                return retorno;
            }



        }





    }
}
