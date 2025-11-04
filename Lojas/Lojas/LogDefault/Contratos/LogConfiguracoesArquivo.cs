using System;

namespace LogDefault
{
    public class LogConfiguracoesArquivo
    {
        public LogSistema Sistema { get; set; }
        public LogArquivo Arquivo { get; set; }
        public LogDiretorio Diretorios { get; set; }

        #region properties
        public class LogSistema
        {
            public string Nome { get; set; }
        }
        public class LogArquivo
        {
            public string Agrupamento { get; set; }
            public string Nome { get; set; }
            public string Extensao { get; set; }

        }
        public class LogDiretorio
        {
            public string Ambiente { get; set; }
            public string Producao { get; set; }
            public string Padrao { get; set; }
            public string Homologacao { get; set; }
            public string Desenvolvimento { get; set; }
            public string Teste { get; set; }
        }

        #endregion

    }
}
