using Microsoft.AspNetCore.Mvc;
using Produtos;
namespace Site.Controllers.Produtos
{
    public class CamisetasController : Controller
    {
        private readonly ProdutoRepositorio _produtoRepositorio;

        // Injeção de dependência para o repositório simulado
        public CamisetasController(ProdutoRepositorio produtoRepositorio)
        {
            _produtoRepositorio = produtoRepositorio;
        }

        // Tela de upload
        public IActionResult Index()
        {
            return View();
        }

    }
}
