using Autenticacao.Settings;
using MercadoPago;
using Microsoft.AspNetCore.Mvc;
using Modelos;
using Newtonsoft.Json;
using Pagamentos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using ViewModels;

namespace Site.Controllers
{
    public class PagamentoController : Controller
    {

        private readonly IPagamentoOpcaoServico _pagamentoOpcaoServico;
        private readonly MercadoPagoService _mercadoPagoService;
        private readonly MercadoPagoSettings _mercadoPagoSettings;
        private readonly HttpClient _httpClient;

        public PagamentoController(IPagamentoOpcaoServico pagamentoOpcaoServico, MercadoPagoService mercadoPagoService
            , HttpClient httpClient, MercadoPagoSettings mercadoPagoSettings)
        {
            _pagamentoOpcaoServico = pagamentoOpcaoServico;
            _mercadoPagoService = mercadoPagoService;
            _httpClient = httpClient;
            _mercadoPagoSettings = mercadoPagoSettings;
        }

        public void Index()
        {

        }

        public async Task<IActionResult> Opcoes()
        {
            try
            {
                var retorno = await _pagamentoOpcaoServico.Listar();
                return View(retorno);
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest("Ocorreu um erro ao listar opções de pagamentos. Tente mais tarde");
            }

        }


    }
}
