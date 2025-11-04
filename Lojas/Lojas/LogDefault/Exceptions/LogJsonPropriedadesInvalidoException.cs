using System;

namespace LogDefault
{
    public class LogJsonPropriedadesInvalidoException : Exception
    {
        public LogJsonPropriedadesInvalidoException()
        {
        }

        public LogJsonPropriedadesInvalidoException(string message)
            : base(message)
        {
        }

        public LogJsonPropriedadesInvalidoException(string message, Exception inner)
            : base(message, inner)
        {
        }

        public static readonly string Mensagem =
            string.Concat("Arquivo de propriedades JSON não encontrado em: "
                , ObterLocalOndeArquivosDePropriedadesDeveriamExistir());

        private static string ObterLocalOndeArquivosDePropriedadesDeveriamExistir()
        {
            string locais = string.Empty;
            if (LogPropriedades.LOCALIZACAO_ARQUIVOS_CONFIG != null && LogPropriedades.LOCALIZACAO_ARQUIVOS_CONFIG.Length > 0)
            {
                locais = String.Join(" ou ", LogPropriedades.LOCALIZACAO_ARQUIVOS_CONFIG);
            }

            return locais;

        }
    }
}
