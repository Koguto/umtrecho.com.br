using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
namespace Testes.Unitarios.MercadoPago
{

    class Program
    {
        private static readonly HttpClient _httpClient = new HttpClient();

        static async Task Main(string[] args)
        {
            //var usuarioId = await ObterUsuarioID();
            //await CriarLoja(usuarioId);

            //await ObterLojaUsuarioID("2147672865");
            //await CriarCaixaUsuarioID();
            await ObterCaixasUsuarioID();
        }



        static async Task ObterLojaUsuarioID(string usuarioId)
        {
            string accessToken = "TEST-6069162952464373-122619-27de34259d1d460432038dd1b3768ee2-2147672865"; // Substitua pelo seu token de acesso do Mercado Pago.

            string userId = usuarioId; // Substitua pelo ID do usuário
            string externalId = "CEB001"; // Substitua pelo `external_id` da loja que deseja buscar

            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    // Configurar o cabeçalho de autorização
                    httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");

                    // URL para buscar a loja pelo `external_id`
                    string url = $"https://api.mercadopago.com/users/{userId}/stores/search?external_id={externalId}";

                    // Fazer a requisição GET
                    HttpResponseMessage response = await httpClient.GetAsync(url);

                    // Verificar se a requisição foi bem-sucedida
                    if (response.IsSuccessStatusCode)
                    {
                        string responseBody = await response.Content.ReadAsStringAsync();
                        Console.WriteLine("Loja encontrada:");
                        Console.WriteLine(responseBody);
                    }
                    else
                    {
                        string errorResponse = await response.Content.ReadAsStringAsync();
                        Console.WriteLine($"Erro ao buscar a loja: {response.StatusCode}");
                        Console.WriteLine(errorResponse);
                        //Saída:  Loja encontrada:
                        //{ "paging":{ "total":1,"offset":0,"limit":0},"results":[{
                        //"id":"65013837",
                        //"name":"Contos e-Books",
                        //"date_creation":"2024-12-28T17:57:57.833Z","business_hours":{
                        //"monday":[{ "open":"09:00","close":"18:00"}],"tuesday":[{ "open":"09:00","close":"18:00"}],
                        //"wednesday":[{ "open":"09:00","close":"18:00"}],"thursday":[{ "open":"09:00","close":"18:00"}],
                        //"friday":[{ "open":"09:00","close":"18:00"}],"saturday":[{ "open":"09:00","close":"18:00"}],
                        //"sunday":[{ "open":"09:00","close":"18:00"}]},"location":{
                        //"address_line":"Rua Engenheiro José Salles 200, São Paulo, São Paulo, Brasil",
                        //"reference":"Perto do PegPese","latitude":-23.6906634,"longitude":-46.6998618,"id":
                        //"BR-SP-44","type":"city","city":"São Paulo","state_id":"BR-SP"},"external_id":"CEB001"}]}
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro: {ex.Message}");
            }

        }



        static async Task ObterCaixasUsuarioID()
        {
            string accessToken = "TEST-6069162952464373-122619-27de34259d1d460432038dd1b3768ee2-2147672865"; // Substitua pelo seu token de acesso do Mercado Pago.


            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    // Configurar o cabeçalho de autorização
                    httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");

                    // URL para buscar a loja pelo `external_id`
                    string url = $"https://api.mercadopago.com/pos";

                    // Fazer a requisição GET
                    HttpResponseMessage response = await httpClient.GetAsync(url);

                    // Verificar se a requisição foi bem-sucedida
                    if (response.IsSuccessStatusCode)
                    {
                        string responseBody = await response.Content.ReadAsStringAsync();
                        Console.WriteLine("POS encontrado:");
                        Console.WriteLine(responseBody);
                    }
                    else
                    {
                        string errorResponse = await response.Content.ReadAsStringAsync();
                        Console.WriteLine($"Erro ao buscar a loja: {response.StatusCode}");
                        Console.WriteLine(errorResponse);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro: {ex.Message}");
            }

        }

        static async Task CriarCaixaUsuarioID()
        {
            string accessToken = "TEST-6069162952464373-122619-27de34259d1d460432038dd1b3768ee2-2147672865"; // Substitua pelo seu token de acesso do Mercado Pago.
            string store_Id = "65013837"; //retornado pelo cadastro da loja

            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    // Defina os dados da loja
                    var primeiroPontoVenda = new
                    {
                        name = "Primeiro POS",
                        fixed_amount = true,
                        store_id = store_Id, // ID da loja gerado pela API
                        external_store_id = "CEB001", // Identificador da loja no seu sistema
                        external_id = "CEBPOSEXT001", // Identificador do POS no seu sistema
                    };


                    var jsonPayload = JsonConvert.SerializeObject(primeiroPontoVenda);
                    var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

                    var url = $"https://api.mercadopago.com/pos";
                    using HttpClient _httpClient = new HttpClient();

                    _httpClient.DefaultRequestHeaders.Clear();
                    _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");

                    var response = await _httpClient.PostAsync(url, content);

                    if (!response.IsSuccessStatusCode)
                    {
                        var errorResponse = await response.Content.ReadAsStringAsync();
                        Console.WriteLine("Erro ao criar a POS: " + errorResponse);
                        throw new Exception("Erro ao criar a POS no Mercado Pago");
                    }

                    var responseBody = await response.Content.ReadAsStringAsync();
                    dynamic responseData = JsonConvert.DeserializeObject(responseBody);

                    // Exibindo a resposta para saber se a loja foi criada com sucesso
                    Console.WriteLine("POS criada com sucesso! Dados da POS: " + responseData);


                    //         POS criada com sucesso!Dados da POS: {
                    //             "id": 108060385,
                    //              "qr": {
                    //                "image": "https://www.mercadopago.com/instore/merchant/qr/108060385/fb18c88508194bf8a3b24451837f69a61332829d46064ea48129c50ed5eb7594.png",
                    //                "template_document": "https://www.mercadopago.com/instore/merchant/qr/108060385/template_fb18c88508194bf8a3b24451837f69a61332829d46064ea48129c50ed5eb7594.pdf",
                    //                "template_image": "https://www.mercadopago.com/instore/merchant/qr/108060385/template_fb18c88508194bf8a3b24451837f69a61332829d46064ea48129c50ed5eb7594.png"
                    //              },
                    //              "status": "active",
                    //              "date_created": "2024-12-28T18:16:40-03:00",
                    //              "date_last_updated": "2024-12-28T18:16:40-03:00",
                    //              "uuid": "fb18c88508194bf8a3b24451837f69a61332829d46064ea48129c50ed5eb7594",
                    //              "user_id": 2147672865,
                    //              "name": "Primeiro POS",
                    //              "fixed_amount": true,
                    //              "store_id": "65013837",
                    //              "external_store_id": "CEB001",
                    //              "external_id": "CEBPOSEXT001",
                    //              "site": "MLB",
                    //              "qr_code": "00020101021226900014BR.GOV.BCB.PIX2568pix-qr.mercadopago.com/instore/p/v2/fb18c88508194bf8a3b24451837f69a643540016com.mercadolibre0130https://mpago.la/pos/1080603855204000053039865802BR5923Diego Buchar Montenegro6009SAO PAULO62070503***63047A8A"
                    //}

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro: {ex.Message}");
            }

        }

        static async Task<string> ObterUsuarioID()
        {
            string accessToken = "TEST-6069162952464373-122619-27de34259d1d460432038dd1b3768ee2-2147672865"; // Substitua pelo seu token de acesso do Mercado Pago.

            try
            {
                // Configuração da URL do endpoint
                string url = "https://api.mercadopago.com/users/me";

                // Configurar cabeçalho de autorização
                _httpClient.DefaultRequestHeaders.Clear();
                _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");

                // Fazer a requisição GET
                var response = await _httpClient.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"Erro na requisição: {response.StatusCode}");
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Detalhes do erro: {errorMessage}");
                    return string.Empty;
                }

                // Ler e exibir o conteúdo da resposta
                var responseBody = await response.Content.ReadAsStringAsync();
                dynamic userData = JsonConvert.DeserializeObject(responseBody);

                Console.WriteLine("\nUsuário autenticado:");
                Console.WriteLine($"ID: {userData.id}");
                Console.WriteLine($"Nome: {userData.first_name} {userData.last_name}");
                Console.WriteLine($"Email: {userData.email}");
                Console.WriteLine($"Site: {userData.site_id}");
                Console.WriteLine($"Tipo de usuário: {userData.user_type}");
                return userData.id;

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro: {ex.Message}");
                throw;
            }
        }


        static async Task CriarLoja(string usuarioId)
        {
            string accessToken = "TEST-6069162952464373-122619-27de34259d1d460432038dd1b3768ee2-2147672865"; // Substitua pelo seu token de acesso do Mercado Pago.

            // Defina os dados da loja
            var lojaData = new
            {
                name = "Contos e-Books",
                business_hours = new
                {
                    monday = new[] { new { open = "09:00", close = "18:00" } },
                    tuesday = new[] { new { open = "09:00", close = "18:00" } },
                    wednesday = new[] { new { open = "09:00", close = "18:00" } },
                    thursday = new[] { new { open = "09:00", close = "18:00" } },
                    friday = new[] { new { open = "09:00", close = "18:00" } },
                    saturday = new[] { new { open = "09:00", close = "18:00" } },  // Exemplo de sábado
                    sunday = new[] { new { open = "09:00", close = "18:00" } }   // Exemplo de domingo
                },
                external_id = "CEB001",
                location = new
                {
                    street_number = "200",
                    street_name = "Rua Engenheiro José Salles",
                    city_name = "São Paulo",
                    state_name = "São Paulo",
                    latitude = -23.6906634,
                    longitude = -46.6998618,
                    reference = "Perto do PegPese"
                }
            };

            var jsonPayload = JsonConvert.SerializeObject(lojaData);
            var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

            // Defina o usuário do vendedor para a URL (substitua pelo ID correto)
            string userId = usuarioId; // Exemplo de user_id, substitua pelo seu ID de vendedor real

            // Enviar a requisição POST para criar a loja
            var url = $"https://api.mercadopago.com/users/{userId}/stores";
            using HttpClient _httpClient = new HttpClient();

            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");

            var response = await _httpClient.PostAsync(url, content);

            if (!response.IsSuccessStatusCode)
            {
                var errorResponse = await response.Content.ReadAsStringAsync();
                Console.WriteLine("Erro ao criar a loja: " + errorResponse);
                throw new Exception("Erro ao criar a loja no Mercado Pago");
            }

            var responseBody = await response.Content.ReadAsStringAsync();
            dynamic responseData = JsonConvert.DeserializeObject(responseBody);

            // Exibindo a resposta para saber se a loja foi criada com sucesso
            Console.WriteLine("Loja criada com sucesso! Dados da loja: " + responseData);
        }




        //static async Task Main(string[] args)
        //{
        //    string baseUrl = "https://api.mercadopago.com/v1/payment_methods";
        //    string accessToken = "TEST-6069162952464373-122619-27de34259d1d460432038dd1b3768ee2-2147672865"; // Substitua pelo seu token de acesso do Mercado Pago.

        //    using HttpClient client = new HttpClient();

        //    // Adicione o token de autenticação nos headers
        //    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);

        //    try
        //    {
        //        // Faz a requisição GET
        //        HttpResponseMessage response = await client.GetAsync(baseUrl);

        //        // Verifica se a resposta foi bem-sucedida
        //        if (response.IsSuccessStatusCode)
        //        {
        //            string result = await response.Content.ReadAsStringAsync();
        //            Console.WriteLine("Resposta:");
        //            Console.WriteLine(result);
        //        }
        //        else
        //        {
        //            Console.WriteLine($"Erro: {response.StatusCode}");
        //            string errorDetails = await response.Content.ReadAsStringAsync();
        //            Console.WriteLine($"Detalhes: {errorDetails}");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"Exceção: {ex.Message}");
        //    }
        //}
    }


}