using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Produtos
{
    public class ProdutoRepositorio
    {
        // Lista simulada para armazenar imagens (simulando um banco de dados)
        private readonly List<string> _imagensBase64 = new List<string>();

        // Método para simular o salvamento da imagem no banco
        public void SalvarImagem(string base64Image)
        {
            // Simula o salvamento da imagem no banco (aqui estamos apenas armazenando em uma lista)
            _imagensBase64.Add(base64Image);
            Console.WriteLine("Imagem salva no repositório: " + base64Image.Substring(0, 20) + "..."); // Exibe uma parte do base64
        }

        // Método para obter todas as imagens (apenas para mostrar a lista, se necessário)
        public List<string> ObterImagens()
        {
            return _imagensBase64;
        }
    }
}
