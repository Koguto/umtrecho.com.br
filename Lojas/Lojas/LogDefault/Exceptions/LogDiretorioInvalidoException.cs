using System;

namespace LogDefault
{
    public class LogDiretorioInvalidoException : Exception
    {
        public LogDiretorioInvalidoException()
        {
        }

        public LogDiretorioInvalidoException(string message)
            : base(message)
        {
        }

        public LogDiretorioInvalidoException(string message, Exception inner)
            : base(message, inner)
        {
        }

        public static readonly string Mensagem =
            string.Concat("Diretório do logg inválido ( "
                , "Local propriedades log: (diretorio acima): ", LogPropriedades.LOCALIZACAO_ARQUIVOS_CONFIG[0]
                , "Local propriedades log: (mesmo diretorio): ", LogPropriedades.LOCALIZACAO_ARQUIVOS_CONFIG[1]
                , " ) Verificar se a pasta ou permissão existem para a determinada pasta.");
    }
}
