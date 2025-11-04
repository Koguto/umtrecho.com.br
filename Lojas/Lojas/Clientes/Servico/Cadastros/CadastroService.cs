using Autenticacao;
using Cliente;
using DAL;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ViewModels;

namespace Clientes.Servico.Cadastro
{
    public class CadastroService : ICadastroService
    {
        private readonly ICadastroRepository _repository;

        public CadastroService(ICadastroRepository repository)
        {
            _repository = repository;
        }

        // Método para obter todos os cadastros
        public async Task<List<CadastroViewModel>> GetAllAsync()
        {///converter

            var models = new List<CadastroViewModel>();

            var lista = await _repository.GetAllAsync();

            foreach (var cadastro in lista)
            {
                var model = new CadastroViewModel
                {
                    Nome = cadastro.Nome,
                    Email = cadastro.Email,
                };
                models.Add(model);
            }

            return models;
        }

        // Método para obter um cadastro por ID
        public async Task<CadastroViewModel> GetByIdAsync(int id)
        {
            var cadastro = await _repository.GetByIdAsync(id);

            var cadastroViewModel = new CadastroViewModel
            {
                Email = cadastro.Email,
                Nome = cadastro.Nome

            };

            return cadastroViewModel;
        }

        // Método para adicionar um novo cadastro
        public async Task CreateAsync(CadastroViewModel cadastro)
        {
            var entidade = new DAL.Modelos.Cadastro();
            entidade.DataCadastro = DateTime.Now.ToString("dd/MM/yyyy");
            entidade.Nome = cadastro.Nome;
            entidade.Email = cadastro.Email;
            byte[] salt = Criptografia.GenerateSalt();

            // Criptografar a senha com salt
            string senhaCriptografadaComSalt = Criptografia.GerarSenhaEncrypt(cadastro.Senha, salt);

            entidade.PasswordHash = senhaCriptografadaComSalt;
            entidade.Salt = Convert.ToBase64String(salt);

            await _repository.AddAsync(entidade);
        }


        /// <summary>
        ///  USAR JWT
        /// </summary>
        /// <param name="atualizar"></param>
        /// <returns></returns>
        // Método para atualizar um cadastro
        public async Task UpdateAsync(CadastroViewModel cadastro)
        {
            var entidade = new DAL.Modelos.Cadastro();
            entidade.DataCadastro = DateTime.Now.ToString("dd/MM/yyyy");
            entidade.Nome = cadastro.Nome;
            entidade.Email = cadastro.Email;
            byte[] salt = Criptografia.GenerateSalt();
            // Criptografar a senha com salt
            //string senhaCriptografadaComSalt = Criptografia.GerarSenhaEncrypt(cadastro.Senha, salt);
            //entidade.PasswordHash = senhaCriptografadaComSalt;
            entidade.Salt = Convert.ToBase64String(salt);

            await _repository.UpdateAsync(entidade);
        }

        /// <summary>
        ///  USAR JWT
        /// </summary>
        /// <param name="deletar"></param>
        /// <returns></returns>
        // Método para excluir um cadastro
        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }
        public async Task DeleteByEmailAsync(string email)
        {
            await _repository.DeleteByEmailAsync(email);
        }


        public async Task<CadastroViewModel> GetAsync(string email)
        {
            var cadastro = await _repository.GetByEmailAsync(email);
            var view = new CadastroViewModel();

            if (cadastro == null)
            {
                return view;
            }

            view.Email = email;
            view.Nome = cadastro.Nome;
            view.Id = cadastro.Id.ToString();

            return view;
        }
    }
}
