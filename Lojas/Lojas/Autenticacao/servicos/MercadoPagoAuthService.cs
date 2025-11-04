namespace Autenticacao.servicos
{
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Text.Json;
    using System.Threading.Tasks;

    public class MercadoPagoAuthService
    {
        private readonly HttpClient _client = new HttpClient();

        public async Task<string> GetBearerTokenAsync(string clientId, string clientSecret)
        {
            var url = "https://api.mercadopago.com/oauth/token";
            var parameters = new Dictionary<string, string>
        {
            { "client_id", clientId },
            { "client_secret", clientSecret },
            { "grant_type", "client_credentials" }
        };

            var content = new FormUrlEncodedContent(parameters);
            var response = await _client.PostAsync(url, content);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var json = JsonDocument.Parse(responseContent);
                var accessToken = json.RootElement.GetProperty("access_token").GetString();
                return accessToken;
            }
            else
            {
                throw new HttpRequestException($"Erro na requisição: {response.StatusCode}");
            }
        }
    }

}