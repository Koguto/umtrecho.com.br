using System;
using System.Collections.Generic;
using System.Text;

namespace LogDefault
{
    /// <summary>
    /// Interfacear as chamadas após obter o app tag
    /// </summary>
    public class LogServico : ILogServico
    {
        #region comportamentos

        #region Local: [Arquivo]
        public string Debug(string mensagem, string tag_app)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add(R.CAMPOS.OBRIGATORIOS.MENSAGEM, mensagem);
            string resultado = LogRegistrador<Dictionary<string, string>>
                .Instancia
                .RegistrarDebug(dic, tag_app);

            return resultado;
        }


        public string Error(string mensagem, string tag_app)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add(R.CAMPOS.OBRIGATORIOS.MENSAGEM, mensagem);
            string resultado = LogRegistrador<Dictionary<string, string>>
                .Instancia
                .RegistrarError(dic, tag_app);
            return resultado;
        }

        public string Error(Exception ex, string tag_app)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add(R.CAMPOS.OBRIGATORIOS.MENSAGEM
                , string.Concat("exeption: ", ex.Message, "InnerException: ", ex.InnerException != null ? ex.InnerException.Message : string.Empty));
            string resultado = LogRegistrador<Dictionary<string, string>>
                .Instancia
                .RegistrarError(dic, tag_app);
            return resultado;
        }

        public string Info(string mensagem, string tag_app)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add(R.CAMPOS.OBRIGATORIOS.MENSAGEM, mensagem);
            string resultado = LogRegistrador<Dictionary<string, string>>
                .Instancia
                .RegistrarInfo(dic, tag_app);
            return resultado;
        }

        public string Warn(string mensagem, string tag_app)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add(R.CAMPOS.OBRIGATORIOS.MENSAGEM, mensagem);
            string resultado = LogRegistrador<Dictionary<string, string>>
                .Instancia
                .RegistrarWarn(dic, tag_app);
            return resultado;
        }

        public string Fatal(string mensagem, string tag_app)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add(R.CAMPOS.OBRIGATORIOS.MENSAGEM, mensagem);

            string resultado = LogRegistrador<Dictionary<string, string>>
                .Instancia
                .RegistrarFatal(dic, tag_app);
            return resultado;
        }

        public string Default(string mensagem, string tag_app)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add(R.CAMPOS.OBRIGATORIOS.MENSAGEM, mensagem);
            string resultado = LogRegistrador<Dictionary<string, string>>
                .Instancia
                .RegistrarDefault(dic, tag_app);
            return resultado;
        }



        #region Compatibilidade Componente anterior

        public string Error(StringBuilder mensagem, string tag_app)
        {
            return this.Error(mensagem.ToString(), tag_app);
        }
        public string Info(StringBuilder mensagem, string tag_app)
        {
            return this.Info(mensagem.ToString(), tag_app);

        }
        public string Warn(StringBuilder mensagem, string tag_app)
        {
            return this.Warn(mensagem.ToString(), tag_app);
        }

        public string Fatal(StringBuilder mensagem, string tag_app)
        {

            return this.Fatal(mensagem.ToString(), tag_app);
        }

        public string Default(StringBuilder mensagem, string tag_app)
        {
            return this.Default(mensagem.ToString(), tag_app);

        }
        #endregion


        #endregion



        #endregion


    }
}
