using Cliente;
using Clientes;
using Emails;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Modelos;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tokens.Servicos;
using ViewModels;

namespace Site.Controllers.Usuario
{

    public class ContaController : Controller
    {
        #region propriedades
        private readonly EmailService _emailService;
        private static string _verificationCode; // Simples para exemplo; em produção, use banco de dados.
        private readonly ICadastroService _cadastroService;
        private readonly ILoginService _loginService;
        private readonly ITokenService _tokenService;
        private readonly PerfilService _perfilService;
        #endregion

        #region construtor
        public ContaController(EmailService emailService
                                , ICadastroService cadastroService
                                , ILoginService loginService
                                , ITokenService tokenService
                                , PerfilService perfilService)
        {
            _emailService = emailService;
            _cadastroService = cadastroService;
            _loginService = loginService;
            _tokenService = tokenService;
            _perfilService = perfilService;
        }

        #endregion


        // GET: /conta/cadastrar
        [HttpGet]
        public IActionResult Cadastro()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Cadastro(CadastroViewModel cadastro)
        {
            if (!ModelState.IsValid)
            {
                return View(cadastro); // Retorna para a view com os erros de validação, caso existam
            }

            // Gerar código de verificação
            _verificationCode = new Random().Next(100000, 999999).ToString();

            // Enviar código por e-mail
            await _emailService.SendEmailAsync(cadastro.Email, "Código de Verificação", $"Seu código de verificação é: {_verificationCode}");

            // Armazenar os dados do cadastro em TempData
            TempData["Cadastro"] = JsonConvert.SerializeObject(cadastro); // Serializa o objeto para armazenar em TempData

            return RedirectToAction("Verificacao"); // Redireciona para a tela de verificação
        }


        [HttpGet]
        public IActionResult Verificacao()
        {
            return View();

        }

        [HttpPost]
        public async Task<IActionResult> Verificacao(string code)
        {
            try
            {
                // Verificar o código
                if (code == _verificationCode)
                {
                    // Recuperar o cadastro armazenado em TempData
                    var cadastroJson = TempData["Cadastro"] as string;
                    if (string.IsNullOrEmpty(cadastroJson))
                    {
                        // Caso não tenha encontrado o cadastro (por algum erro de fluxo)
                        return RedirectToAction("Cadastro");
                    }

                    // Desserializar os dados para o modelo de cadastro
                    var cadastro = JsonConvert.DeserializeObject<CadastroViewModel>(cadastroJson);

                    // Agora você pode salvar o cadastro no banco
                    await _cadastroService.CreateAsync(cadastro);

                    var perfilViewModel = new PerfilViewModel();
                    perfilViewModel.Email = cadastro.Email;
                    perfilViewModel.Nome = cadastro.Nome;
                    try
                    {
                        //criar um perfil
                        var retorno = _perfilService.Criar(perfilViewModel);


                        if (retorno)
                            return RedirectToAction("Index", "Home"); // Redireciona para a página inicial
                        else
                        {
                            await _cadastroService.DeleteByEmailAsync(cadastro.Email);
                            ModelState.AddModelError(string.Empty, "Erro ao concluir o cadastro.");
                        }
                    }
                    catch (Exception)
                    {
                        await _cadastroService.DeleteByEmailAsync(cadastro.Email);
                        throw;
                    }

                    // Limpar o TempData após o uso
                    TempData.Remove("Cadastro");

                    return RedirectToAction("Sucesso");
                }

                // Caso o código esteja errado
                ModelState.AddModelError("CodigoInvalido", "Código de verificação inválido.");
                return View();
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627 || ex.Number == 2601)
                {
                    // Aqui você pode tratar a violação da constraint UNIQUE
                    ModelState.AddModelError("CodigoInvalido", "já existe um cadastro com este email.");
                    Console.WriteLine("Violação de chave única.");
                    return BadRequest();
                }
                else
                {
                    // Tratar outros tipos de erro SQL
                    Console.WriteLine($"Erro SQL: {ex.Message}");
                    ModelState.AddModelError("CodigoInvalido", "Falha de comunicação ao criar o usuário");
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {

                ModelState.AddModelError("CodigoInvalido", "Ocorreu um erro tente mais tarde.");
               return BadRequest();
            }
        }




        // Exibir a página de login
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // Processar o login do usuário
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var sucesso = await _loginService.LogarAsync(model);
                    // Exemplo simples de validação de usuário
                    // Aqui você deve validar contra o banco de dados
                    if (sucesso)
                    {
                        //Gerar JWT
                        if (true)
                        {
                            var cadastro = await _cadastroService.GetAsync(model.Email);
                            if (cadastro.Id == null)
                            {
                                ModelState.AddModelError(string.Empty, "Cadastro não encontrado. Efetue um cadastro.");
                                return View(model); // Caso falhe, retorna à mesma página com o erro
                            }

                            var payload = new PayloadUsuario();
                            payload.Email = cadastro.Email;
                            payload.Nome = cadastro.Nome;
                            payload.ID = Convert.ToInt32(cadastro.Id);

                            var token = _tokenService.GerarJWT(payload);


                            // Adicionando o token JWT ao cookie
                            var cookieOptions = new CookieOptions
                            {
                                HttpOnly = true,  // Impede que o JavaScript acesse o cookie
                                Secure = true,    // Garante que o cookie só será transmitido via HTTPS
                                SameSite = SameSiteMode.Lax, // Controla quando o cookie será enviado
                                Expires = DateTime.UtcNow.AddDays(7) // Define a expiração do cookie
                            };

                            Response.Cookies.Append("jwt", token, cookieOptions);
                            return RedirectToAction("index", "home");

                        }
                        // Se o login for bem-sucedido
                    }
                    else
                    {
                        // Se o login falhar
                        ModelState.AddModelError(string.Empty, "Usuário ou senha inválidos.");
                    }
                }

                return View(model); // Caso falhe, retorna à mesma página com o erro
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Ocorreu um erro ao acessar seu login");
                return View();
            }
        }
        public IActionResult Logout()
        {
            // Remove o cookie JWT definindo o mesmo nome usado para o cookie
            Response.Cookies.Delete("jwt", new CookieOptions
            {
                Path = "/",      // Certifica-se de que o cookie seja removido de todo o caminho
                Secure = true,   // Se o cookie for Secure (HTTPS)
                HttpOnly = true  // Se o cookie for HttpOnly
            });

            // Opcional: Limpa a sessão se estiver usando
            //HttpContext.Session.Clear();

            // Redireciona para a página inicial ou de login
            return RedirectToAction("Index", "Home");
        }



        // Ação para exibir a página de sucesso
        public IActionResult Sucesso()
        {
            return View();
        }



    }

}
























//// Atualizar um cadastro existente
//[HttpPut("{id}")]
//public async Task<IActionResult> Update(int id, [FromBody] Cadastro cadastro)
//{
//    if (id != cadastro.Id)
//    {
//        return BadRequest();
//    }
//    await _service.UpdateAsync(cadastro);
//    return NoContent();
//}

//// Deletar um cadastro
//[HttpDelete("{id}")]
//public async Task<IActionResult> Delete(int id)
//{
//    await _service.DeleteAsync(id);
//    return NoContent();
//}


//// Obter todos os cadastros
//[HttpGet]
//public async Task<IActionResult> GetAll()
//{
//    var cadastros = await _service.GetAllAsync();
//    return Ok(cadastros);
//}

//// Obter um cadastro por ID
//[HttpGet("{id}")]
//public async Task<IActionResult> GetById(int id)
//{
//    var cadastro = await _service.GetByIdAsync(id);
//    if (cadastro == null)
//    {
//        return NotFound();
//    }
//    return Ok(cadastro);
//}


