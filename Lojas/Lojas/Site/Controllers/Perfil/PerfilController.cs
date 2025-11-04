// Controllers/PerfilController.cs
using Microsoft.AspNetCore.Mvc;
using DAL;
using DAL.Modelos;
using ViewModels;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;
using Clientes;
using Tokens;

namespace Site.Controllers
{
    public class PerfilController : Controller
    {
        private readonly LojasContext _context;
        private readonly PerfilService _perfilService;
        private readonly IToken _token;
        

        public PerfilController(LojasContext context, PerfilService perfilService, IToken token)
        {
            _context = context;
            _perfilService = perfilService;
            _token = token;
        }

        // GET: Perfil/Index
        public IActionResult Index()
        {
            var token = Request.Cookies["jwt"];
            if (token == null) RedirectPermanent("home");
            var payloadLogin = _token.LerJwtLogin(token);
            var payload = payloadLogin.Propriedades;

            var perfil = _perfilService.ObterPerfilComDetalhes(payload.Email);
            perfil.Nome = payload.Nome;
            perfil.Email = payload.Email;

            return View(perfil);
        }


        // GET: Perfil/Criar
        [HttpGet]
        public IActionResult Criar()
        {
            return View();
        }

        // POST: Perfil/Criar
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Criar(PerfilViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            _perfilService.AtualizarPerfil(viewModel);

            TempData["MensagemSucesso"] = "Perfil atualizado com sucesso!";
            return RedirectToAction("index", "perfil");
        }

        // GET: Perfil/Editar/{id}
        [HttpGet]
        public async Task<IActionResult> Editar(int id)
        {
            var perfil = await _context.Perfils
                .Include(p => p.Enderecos)
                //.Include(p => p.FormaPagamentos)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (perfil == null)
            {
                return NotFound();
            }

            var viewModel = new PerfilViewModel
            {
                Id = perfil.Id,
                Nome = perfil.Nome,
                Email = perfil.Email,
                Endereco = perfil.Enderecos.Select(e => new EnderecoViewModel
                {
                    Rua = e.Rua,
                    Numero = e.Numero,
                    Complemento = e.Complemento,
                    Bairro = e.Bairro,
                    Cidade = e.Cidade,
                    Estado = e.Estado,
                    CEP = e.Cep
                }).FirstOrDefault()
                //,
                //FormaPagamento = perfil.FormaPagamentos.Select(f => new FormaPagamentoViewModel
                //{
                //    NumeroCartao = f.NumeroCartao,
                //    NomeTitular = f.NomeTitular,
                //    Validade = f.Validade,
                //    CVV = f.Cvv
                //}).FirstOrDefault()
            };

            return View(viewModel);
        }

        // POST: Perfil/Editar/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(int id, PerfilViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var perfil = await _context.Perfils
                .Include(p => p.Enderecos)
                //.Include(p => p.FormaPagamentos)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (perfil == null)
            {
                return NotFound();
            }

            perfil.Nome = viewModel.Nome;
            perfil.Email = viewModel.Email;

            var endereco = perfil.Enderecos.FirstOrDefault();
            if (endereco != null)
            {
                endereco.Rua = viewModel.Endereco.Rua;
                endereco.Numero = viewModel.Endereco.Numero;
                endereco.Complemento = viewModel.Endereco.Complemento;
                endereco.Bairro = viewModel.Endereco.Bairro;
                endereco.Cidade = viewModel.Endereco.Cidade;
                endereco.Estado = viewModel.Endereco.Estado;
                endereco.Cep = viewModel.Endereco.CEP;
            }

            //var formaPagamento = perfil.FormaPagamentos.FirstOrDefault();
            //if (formaPagamento != null)
            //{
            //    formaPagamento.NumeroCartao = viewModel.FormaPagamento.NumeroCartao;
            //    formaPagamento.NomeTitular = viewModel.FormaPagamento.NomeTitular;
            //    formaPagamento.Validade = viewModel.FormaPagamento.Validade;
            //    formaPagamento.Cvv = viewModel.FormaPagamento.CVV;
            //}

            _context.Perfils.Update(perfil);
            await _context.SaveChangesAsync();

            TempData["MensagemSucesso"] = "Perfil atualizado com sucesso!";
            return RedirectToAction(nameof(Index));
        }

        // POST: Perfil/Excluir/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Excluir(int id)
        {
            var perfil = await _context.Perfils
                .Include(p => p.Enderecos)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (perfil == null)
            {
                TempData["MensagemErro"] = "Perfil não encontrado.";
                return RedirectToAction(nameof(Index));
            }

            _context.Perfils.Remove(perfil);
            await _context.SaveChangesAsync();

            TempData["MensagemSucesso"] = "Perfil excluído com sucesso!";
            return RedirectToAction(nameof(Index));
        }


    }
}
