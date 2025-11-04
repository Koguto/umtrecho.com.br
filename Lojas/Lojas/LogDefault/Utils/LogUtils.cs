using System;
namespace LogDefault
{
    #region para "generic types"
    public static class LogUtils
    {
        public struct Tag
        {
            public string Nome { get; set; }
        }

        public struct IndexChaveValor
        {
            public string Index { get; set; }
            public string Chave { get; set; }
            public string Valor { get; set; }
        }
        public class LoggRetorno
        {
            /// <summary>
            /// Propriedade responsável por:
            ///     - exibir 1 sucesso
            ///     - exibir 2 falha
            /// </summary>
            public bool Sucesso { get; set; }
            public string Descricao { get; set; }
            public Exception Exception { get; set; }
            public string[] LocalArquivoConfiguracao { get; set; }

        }

    }
    #endregion



}
