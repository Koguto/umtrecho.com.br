using Microsoft.AspNetCore.Mvc;
using Produtos;
namespace Site.Controllers.Produtos
{
    public class CanecasController : Controller
    {
        private readonly ProdutoRepositorio _produtoRepositorio;

        // Injeção de dependência para o repositório simulado
        public CanecasController(ProdutoRepositorio produtoRepositorio)
        {
            _produtoRepositorio = produtoRepositorio;
        }

        public IActionResult Index()
        {
            return View();
        }

    }
}
