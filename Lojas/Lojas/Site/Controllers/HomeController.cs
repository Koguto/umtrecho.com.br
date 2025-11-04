using DAL.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Site.Models;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ViewModels;

namespace Site.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly LojasContext lojasContext;

        public HomeController(LojasContext context, ILogger<HomeController> logger)
        {
            _logger = logger;
            lojasContext = context;

        }


        public async Task<IActionResult> Index()
        {
            var token = Request.Cookies["jwt"];

            // Obter categorias e seus produtos associados
            var categoriasProdutos = await lojasContext.Categorias
                .Include(c => c.Produtos)  // Carrega produtos associados à categoria
                .ToListAsync();

            // Mapeia para a lista de ViewModels
            var categoriasProdutosViewModel = categoriasProdutos.Select(categoria => new CategoriaViewModel
            {
                CategoriaId = categoria.CategoriaId,
                Nome = categoria.Nome,
                Produtos = categoria.Produtos.Select(produto =>
                {

                    var produtoViewModel = new ProdutoViewModel
                    {
                        ProdutoId = produto.ProdutoId,
                        Codigo = produto.Codigo,
                        Nome = produto.Nome,
                        Preco = produto.Preco,
                        QuantidadeEstoque = produto.QuantidadeEstoque.Value,
                        ImagemUrl = produto.ImagemUrl
                    };
                    if (categoria.CategoriaId == 1)
                    {
                        produtoViewModel.LinkCompra = produto.LinkCompra + produto.Codigo;
                    }
                    else {
                        produtoViewModel.LinkCompra = produto.LinkCompra;
                    }
                    return produtoViewModel;
                }

                ).ToList()
            }).ToList();


#if DEBUG
            // Realizar o replace após carregar os dados
            foreach (var categoria in categoriasProdutosViewModel)
            {
                foreach (var produto in categoria.Produtos)
                {
                    // Substituindo "mofya" por "localhost:44378" na URL da imagem
                    if (produto.ImagemUrl != null)
                    {
                        produto.ImagemUrl = produto.ImagemUrl.Replace("www.umtrecho.com.br", "localhost:44378");
                    }

                    if (produto.LinkCompra != null)
                    {
                        produto.LinkCompra = produto.LinkCompra.Replace("www.umtrecho.com.br", "localhost:44378");
                    }

                }
            }
#endif

            return View(categoriasProdutosViewModel.OrderByDescending(x => x.CategoriaId == 2));  // Passa as categorias e produtos para a View
        }


        // Ação para verificar a existência do JWT no cookie
        [HttpGet]
        public IActionResult CheckJwtCookie()
        {
            var jwtToken = Request.Cookies["jwt"];

            // Retorna uma resposta JSON indicando se o token JWT existe
            return Json(new { isAuthenticated = !string.IsNullOrEmpty(jwtToken) });
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
