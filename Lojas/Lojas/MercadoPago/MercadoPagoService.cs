using Autenticacao.Settings;
using Microsoft.Extensions.Options;
using Modelos;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace MercadoPago
{
    public class MercadoPagoService
    {
        private readonly MercadoPagoSettings _mercadoPagoSettings;
        private readonly HttpClient _httpClient;

        public MercadoPagoService(HttpClient httpClient, IOptions<MercadoPagoSettings> mercadoPagoSettings)
        {
            _mercadoPagoSettings = mercadoPagoSettings.Value;
            _httpClient = httpClient;
        }



        public async Task<string> CriarPagamentoComPOS(string nomeProduto, decimal preco, int quantidade, string email)
        {
            string accessToken = "TEST-6069162952464373-122619-27de34259d1d460432038dd1b3768ee2-2147672865"; // Substitua pelo seu token de acesso do Mercado Pago.
            try
            {

                using (HttpClient httpClient = new HttpClient())
                {
                    // Configurar o cabeçalho de autorização
                    httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");

                    var requestData = new
                    {
                        external_reference = "ref_caneca_mofay",  // Identificador único do pedido
                        title = "Product order caneca mofay",  // Título do pedido
                        description = "Caneca de porcelana mofay.",  // Descrição do pedido
                        notification_url = "https://www.contosebooks.com/notifications",  // URL para notificações
                        total_amount = (preco * quantidade),  // Valor total do pagamento
                        items = new[]
                    {
                        new
                        {
                            sku_number = "SKUCANECA101001",  // SKU do produto
                            category = "marketplace",  // Categoria do produto
                            title = "Caneca Mofay",  // Título do produto
                            description = "Caneca de porcelana Mofay",  // Descrição do produto
                            unit_price = preco,  // Preço unitário
                            quantity = 1,  // Quantidade
                            unit_measure = "unit",  // Unidade de medida
                            total_amount = (preco * quantidade)  // Valor total do item
                        }
                    }
                    };

                    string jsonRequest = Newtonsoft.Json.JsonConvert.SerializeObject(requestData);
                    StringContent content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
                    //var external_pos_id = "CEBPOSEXT001";
                    //var user_id = "2147672865";
                    HttpResponseMessage response = await httpClient.PutAsync(
                        "https://api.mercadopago.com/instore/orders/qr/seller/collectors/2147672865/pos/CEBPOSEXT001/qrs",
                        content);

                    if (response.IsSuccessStatusCode)
                    {
                        string responseBody = await response.Content.ReadAsStringAsync();
                        Console.WriteLine($"QR Code criado com sucesso: {responseBody}");
                        return responseBody;
                    }
                    else
                    {
                        string errorResponse = await response.Content.ReadAsStringAsync();
                        Console.WriteLine($"Erro ao criar QR Code: {errorResponse}");
                        return errorResponse;
                    }
                }
            }
            catch (Exception ex)
            {

                throw;
            }


        }



        public async Task<string> CriarPreferenciaAsync(string nomeProduto, decimal preco, int quantidade, string email)
        {
            // Definir o payload da requisição
            try
            {



                var payload = new
                {
                    items = new[]
                    {
                        new
                        {
                            title = nomeProduto,
                            quantity = quantidade,
                            unit_price = preco,
                            currency_id = "BRL"
                        }
                    },
                    payment_methods = new
                    {
                        excluded_payment_types = new[] { new { id = "ticket" } },
                        default_payment_method_id = "pix"
                    },
                    payer_email = email
                    ,
                    back_urls = new
                    {
                        success = _mercadoPagoSettings.Sucesso,
                        failure = _mercadoPagoSettings.Falha,
                        pending = _mercadoPagoSettings.Pendente
                    },
                    auto_return = "approved"
                };

                var accessToken = _mercadoPagoSettings.AccessToken; // Substitua com o seu token

                // Serializar o payload para JSON usando Newtonsoft.Json
                var jsonPayload = JsonConvert.SerializeObject(payload);
                var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");
                // Criar o cabeçalho de autorização
                _httpClient.DefaultRequestHeaders.Clear();
                _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");

                var response = await _httpClient.PostAsync("https://api.mercadopago.com/checkout/preferences", content);

                if (!response.IsSuccessStatusCode)
                {
                    var errorResponse = await response.Content.ReadAsStringAsync();
                    // Logue o erro completo para ajudar na depuração
                    Console.WriteLine("Erro: " + errorResponse);
                    throw new Exception("Erro ao criar a preferência no Mercado Pago");
                }

                var responseBody = await response.Content.ReadAsStringAsync();
                dynamic responseData = JsonConvert.DeserializeObject(responseBody);
                string preferenceId = responseData.id; // ID da preferência

                // Agora, obter o payment_id associado à preferência
                string paymentId = await ObterPaymentIdAsync(preferenceId, accessToken); // Obtenha o payment_id relacionado à preferência

                // Obter o QR Code associado ao payment_id
                var qrCodeUrl = await ObterQrCode(paymentId, accessToken);

                return qrCodeUrl; // Retorna a URL do QR Code

            }
            catch (System.Exception ex)
            {

                throw;
            }
        }

        public async Task<string> ObterPaymentIdAsync(string preferenceId, string accessToken)
        {
            // URL para consultar os pagamentos com base no preferenceId
            var url = $"https://api.mercadopago.com/v1/payments/search?preference_id={preferenceId}";

            // Configurar o cabeçalho de autorização
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");

            // Fazer a requisição GET para obter os pagamentos
            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                var errorResponse = await response.Content.ReadAsStringAsync();
                Console.WriteLine("Erro ao obter os pagamentos: " + errorResponse);
                throw new Exception("Erro ao obter o pagamento associado à preferência.");
            }

            var responseBody = await response.Content.ReadAsStringAsync();
            dynamic responseData = JsonConvert.DeserializeObject(responseBody);

            // Verificar se há pagamentos e retornar o paymentId do primeiro pagamento encontrado
            if (responseData.results != null && responseData.results.Count > 0)
            {
                return responseData.results[0].id; // Retorna o paymentId do primeiro pagamento
            }

            throw new Exception("Nenhum pagamento encontrado para essa preferência.");
        }


        private async Task<string> ObterQrCode(string paymentId, string accessToken)
        {
            // Defina o seu access token do Mercado Pago
            try
            {
                // Configurar a URL da API para obter a preferência
                var url = $"https://api.mercadopago.com/v1/payments/{paymentId}/qr_code";

                // Criar o cabeçalho de autorização
                _httpClient.DefaultRequestHeaders.Clear();
                _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");

                // Fazer a requisição GET para obter os dados do QR Code
                var response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();

                // Ler o conteúdo da resposta
                var responseBody = await response.Content.ReadAsStringAsync();

                // Desserializar a resposta (a URL do QR Code estará dentro da resposta)
                var responseData = JsonConvert.DeserializeObject<dynamic>(responseBody);

                // Retornar a URL do QR Code
                return responseData.qr_code_url; // URL do QR Code para pagamento Pix
            }
            catch (Exception ex)
            {

                throw;
            }
        }




    }

}
