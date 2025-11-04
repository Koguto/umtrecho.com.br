using DAL.Modelos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using ViewModels;

namespace Clientes
{

    public class PerfilService
    {
        private readonly LojasContext _context;

        public PerfilService(LojasContext context)
        {
            _context = context;
        }

        public PerfilViewModel ObterPerfilComDetalhes(string email)
        {
            var perfil = _context.Perfils
                .Include(p => p.Enderecos)
                .FirstOrDefault(p => p.Email == email);

            if (perfil == null) return new PerfilViewModel();

            var enderecos = new List<EnderecoViewModel>();
            perfil.Enderecos.ToList().ForEach(x => enderecos.Add(new EnderecoViewModel
                                                                    {
                                                                        Rua = x.Rua,
                                                                        Numero = x.Numero,
                                                                        Complemento = x.Complemento,
                                                                        Bairro = x.Bairro,
                                                                        Cidade = x.Cidade,
                                                                        Estado = x.Estado,
                                                                        CEP = x.Cep
                                                                    })
            );

            return new PerfilViewModel
            {
                Id = perfil.Id,
                Nome = perfil.Nome,
                Email = perfil.Email,
                EnderecosAdicionados = enderecos,
                Endereco = perfil.Enderecos.FirstOrDefault() != null ? new EnderecoViewModel
                {
                    Rua = perfil.Enderecos.First().Rua,
                    Numero = perfil.Enderecos.First().Numero,
                    Complemento = perfil.Enderecos.First().Complemento,
                    Bairro = perfil.Enderecos.First().Bairro,
                    Cidade = perfil.Enderecos.First().Cidade,
                    Estado = perfil.Enderecos.First().Estado,
                    CEP = perfil.Enderecos.First().Cep
                } : new EnderecoViewModel(),
                //FormaPagamento = perfil.FormaPagamentos.FirstOrDefault() != null ? new FormaPagamentoViewModel
                //{
                //    NumeroCartao = perfil.FormaPagamentos.First().NumeroCartao,
                //    NomeTitular = perfil.FormaPagamentos.First().NomeTitular,
                //    Validade = perfil.FormaPagamentos.First().Validade,
                //    CVV = perfil.FormaPagamentos.First().Cvv
                //} : new FormaPagamentoViewModel()
            };
        }

        public void AtualizarPerfil(PerfilViewModel viewModel)
        {


            var perfilModel = new Perfil
            {
                Nome = viewModel.Nome,
                Email = viewModel.Email,
                Enderecos = new HashSet<Endereco>
                {
                    new Endereco
                    {
                        Rua = viewModel.Endereco.Rua,
                        Numero = viewModel.Endereco.Numero,
                        Complemento = viewModel.Endereco.Complemento,
                        Bairro = viewModel.Endereco.Bairro,
                        Cidade = viewModel.Endereco.Cidade,
                        Estado = viewModel.Endereco.Estado,
                        Cep = viewModel.Endereco.CEP
                    }
                }
            };


            var perfilExistente = _context.Perfils
                .Include(p => p.Enderecos)
                .FirstOrDefault(p => p.Email == viewModel.Email);

            if (perfilExistente == null)
            {
                this.Criar(viewModel);

                perfilExistente = _context.Perfils
                    .Include(p => p.Enderecos)
                    .FirstOrDefault(p => p.Email == viewModel.Email);
            }



            // Atualizar dados do perfil
            perfilExistente.Nome = perfilModel.Nome;
            perfilExistente.Email = perfilModel.Email;

            // Atualizar ou adicionar endereço
            var endereco = perfilExistente.Enderecos.FirstOrDefault();
            if (endereco == null)
            {
                endereco = new Endereco { PerfilId = perfilExistente.Id };

                endereco.Rua = viewModel.Endereco.Rua;
                endereco.Numero = viewModel.Endereco.Numero;
                endereco.Complemento = viewModel.Endereco.Complemento;
                endereco.Bairro = viewModel.Endereco.Bairro;
                endereco.Cidade = viewModel.Endereco.Cidade;
                endereco.Estado = viewModel.Endereco.Estado;
                endereco.Cep = viewModel.Endereco.CEP;

                perfilExistente.Enderecos.Add(endereco);
            }
            else
            {

                endereco.Rua = viewModel.Endereco.Rua;
                endereco.Numero = viewModel.Endereco.Numero;
                endereco.Complemento = viewModel.Endereco.Complemento;
                endereco.Bairro = viewModel.Endereco.Bairro;
                endereco.Cidade = viewModel.Endereco.Cidade;
                endereco.Estado = viewModel.Endereco.Estado;
                endereco.Cep = viewModel.Endereco.CEP;

            }

            var rowsAffected = _context.SaveChanges();

        }


        public bool Criar(PerfilViewModel viewModel)
        {

            try
            {
                var perfil = _context.Perfils
                .Include(p => p.Enderecos)
                .FirstOrDefault(p => p.Email == viewModel.Email);

                if (perfil != null)
                {
                    AtualizarPerfil(viewModel);
                    return true;
                }

                perfil = new Perfil();
                // Atualizar dados do perfil
                perfil.Nome = viewModel.Nome;
                perfil.Email = viewModel.Email;

                // Adiciona o perfil ao contexto (preparando para inserção)
                _context.Add(perfil);

                // Salva as alterações no banco de dados
                var rowsAffected = _context.SaveChanges();

                // Verifica se a operação foi bem-sucedida
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                // Loga o erro (pode ser uma exceção personalizada ou apenas uma Exception)
                // O log pode ser feito com uma ferramenta como o Serilog, NLog, etc.
                // Aqui estamos apenas lançando a exceção novamente para ser tratada na camada do controller
                throw new InvalidOperationException("Erro ao salvar o perfil.", ex);
            }


        }

    }
}