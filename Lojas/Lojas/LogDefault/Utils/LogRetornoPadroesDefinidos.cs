using Newtonsoft.Json;
using System;

namespace LogDefault
{
    public class LogRetornoPadroesDefinidos
    {
        public static string Erro(Exception ex)
        {
            var jsonRetorno = JsonConvert.SerializeObject(ex.Message);
            return jsonRetorno;
        }

        public static string Sucesso(string mensagem)
        {
            var jsonRetorno = JsonConvert.SerializeObject(mensagem);
            return jsonRetorno;
        }

    }
}
