using DAL.Modelos;
using MercadoPago;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Produtos;
using System;
using System.Linq;
using System.Threading.Tasks;
using Tokens;
using ViewModels;

namespace Site.Controllers.Produtos
{
    public class ProdutoController : Controller
    {
        private readonly ProdutoRepositorio _produtoRepositorio;
        private readonly LojasContext _context;
        private readonly MercadoPagoService _mercadoPagoService;
        private readonly IToken _token;

        // Injeção de dependência para o repositório simulado
        public ProdutoController(
            LojasContext context
            , ProdutoRepositorio produtoRepositorio
            , MercadoPagoService mercadoPagoService
            , IToken token)
        {
            _context = context;
            _produtoRepositorio = produtoRepositorio;
            _mercadoPagoService = mercadoPagoService;
            _token = token;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Route("produto/{codigo:int}")]
        public IActionResult Index(int codigo)
        {
            var produto = _context.Produtos.FirstOrDefault(p => p.Codigo == codigo);

            if (produto == null)
            {
                return NotFound("Produto não encontrado.");
            }

            var produtoViewModel = new ProdutoViewModel
            {
                ProdutoId = produto.ProdutoId,
                Codigo = produto.Codigo,
                Nome = produto.Nome,
                Preco = produto.Preco,
                QuantidadeEstoque = produto.QuantidadeEstoque.Value,
                ImagemUrl = produto.ImagemUrl,
                LinkCompra = produto.LinkCompra
            };

            return View(produtoViewModel);
        }
        [HttpPost]
        [Route("produto/Comprar")]
        public async Task<IActionResult> Comprar([FromForm] ProdutoViewModel produto)
        {

            var token = Request.Cookies["jwt"];
            if (token == null) RedirectPermanent("home");
            var payloadLogin = _token.LerJwtLogin(token);
            var payload = payloadLogin.Propriedades;

            // Chamar o serviço do Mercado Pago para criar a preferência
            string qrCodeResponse = await _mercadoPagoService.CriarPagamentoComPOS(
                produto.Nome, produto.Preco, 1, payload.Email); // Quantidade assumida como 1 para este exemplo

            OrderResponseViewModel orderResponseViewModel = JsonConvert.DeserializeObject<OrderResponseViewModel>(qrCodeResponse);

            var qrCodeGenerator = new Helpers.QrCodeGenerator();
            string base64QrCode = qrCodeGenerator.GenerateQrCodeBase64(orderResponseViewModel.QrData);

            // Redirecionar para uma página que exibe o Brick com os dados
            return View("Checkout", new CheckoutViewModel
            {
                Produto = produto,
                QrCodeUrl = base64QrCode

            });
        }


    }
}

