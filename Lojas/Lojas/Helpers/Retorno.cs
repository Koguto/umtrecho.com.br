using Helper.Utils;

namespace Helper
{
    public class Retorno<T>
    {
        public T? Propriedades { get; set; }
        public CODIGO Codigo { get; set; }

        public string? MensagemSucesso { get; set; }
        public string? MensagemErro { get; set; }
        public string? MensagemFalha { get; set; }
        public string? StackTrace { get; set; }
    }
}
