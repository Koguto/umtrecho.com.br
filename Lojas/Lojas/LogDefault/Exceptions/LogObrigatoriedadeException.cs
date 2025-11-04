using System;

namespace LogDefault
{
    public class LogObrigatoriedadeException : Exception
    {
        public LogObrigatoriedadeException()
        {
        }

        public LogObrigatoriedadeException(string message)
            : base(message)
        {
        }

        public LogObrigatoriedadeException(string message, Exception inner)
            : base(message, inner)
        {
        }

        public const string Mensagem = "Chave mensagem é obrigatoria";
    }
}
