using Autenticacao.Settings;
using Microsoft.Extensions.Options;
using Modelos;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Pagamentos
{
    public class PagamentoOpcaoServico : IPagamentoOpcaoServico
    {
        private readonly MercadoPagoSettings _mercadoPagoSettings;

        public PagamentoOpcaoServico(IOptions<MercadoPagoSettings> mercadoPagoSettings)
        {
            _mercadoPagoSettings = mercadoPagoSettings.Value;
        }

        public async Task<List<PagamentoOpcaoModel>> Listar()
        {
            string baseUrl = "https://api.mercadopago.com/v1/payment_methods";
          
            using HttpClient client = new HttpClient();

            // Adicione o token de autenticação nos headers
            client.DefaultRequestHeaders.Authorization = 
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _mercadoPagoSettings.AccessToken);

            try
            {
                // Faz a requisição GET
                HttpResponseMessage response = await client.GetAsync(baseUrl);
                response.EnsureSuccessStatusCode();

                string result = await response.Content.ReadAsStringAsync();
                var paymentOptions = JsonConvert.DeserializeObject<List<PagamentoOpcaoModel>>(result);
                return paymentOptions;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exceção: {ex.Message}");
                throw new Exception(ex.Message);
            }

        }


    }
}
