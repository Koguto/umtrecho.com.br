using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Produtos;
using System;
using System.IO;
using System.Threading.Tasks;
namespace Site.Controllers
{
    public class UploadController : Controller
    {
        private readonly ProdutoRepositorio _produtoRepositorio;

        // Injeção de dependência para o repositório simulado
        public UploadController(ProdutoRepositorio produtoRepositorio)
        {
            _produtoRepositorio = produtoRepositorio;
        }

        // Tela de upload
        public IActionResult Index()
        {
            return View();
        }

        // Ação para fazer upload da imagem e simular o salvamento no banco
        [HttpPost]
        public async Task<IActionResult> UploadImage(IFormFile image)
        {
            if (image != null && image.Length > 0)
            {
                // Converte a imagem para base64
                using (var memoryStream = new MemoryStream())
                {
                    await image.CopyToAsync(memoryStream);
                    var imageBytes = memoryStream.ToArray();
                    var base64Image = Convert.ToBase64String(imageBytes);

                    // Simula o salvamento da imagem no banco
                    _produtoRepositorio.SalvarImagem(base64Image);

                    // Passa a imagem base64 para a View
                    ViewData["ImageBase64"] = base64Image;
                }
            }

            return View("Index");
        }
    }
}
