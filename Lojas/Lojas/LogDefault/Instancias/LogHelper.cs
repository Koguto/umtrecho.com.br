using Helper;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LogDefault
{
    public class LogHelper
    {

        #region Propriedades

        private readonly LogServico logG = LogManager.GetCurrentClassLogger();

        #endregion

        #region Singleton
        private LogHelper()
        {

        }
        private static LogHelper? instancia;
        public static LogHelper Instancia
        {
            get
            {
                if (instancia is null)
                {
                    instancia = new LogHelper();
                }
                return instancia;
            }
        }

        #endregion




        #region Metodos

        #region Error

        public string Error(string mensagem, Exception ex)
        {
            try
            {
                var retorno = logG.Error(string.Concat(mensagem, ex), "siscoe");
                System.Diagnostics.Debug.WriteLine(string.Concat("Error: ", retorno));
                return retorno;
            }
            catch (Exception ex1)
            {
                return string.Concat("Erro no log", ex1.Message);
            }
        }
        public string Error(StringBuilder mensagem, Exception ex)
        {
            try
            {
                var retorno = logG.Error(string.Concat(mensagem, ex), "siscoe");
                System.Diagnostics.Debug.WriteLine(string.Concat("Error(sb): ", retorno));
                return retorno;
            }
            catch (Exception ex1)
            {
                return string.Concat("Error no log2", ex1.Message);
            }
        }

        #endregion

        #region Info

        public Retorno<string> Checar(string msg)
        {
            #region Logg: [Chamada]
            var retorno = LogHelper.Instancia.ChecarFuncionalidade(msg);
            #endregion

            #region Logg: [retorno]

            #region Json: [Parse]
            var loggRetornoObject = JObject.Parse(retorno);
            #endregion

            #region Json: [Informacoes]
            var houveSucessoNaChecagem = loggRetornoObject["Sucesso"].Value<bool>();
            JToken exception;
            List<JToken> exceptionParametros = new List<JToken>();
            var exceptionMsgErro = string.Empty;
            var exceptionMsgErro1 = string.Empty;
            #endregion

            if (!houveSucessoNaChecagem)
            {
                exception = loggRetornoObject["Exception"];
                exceptionParametros.Clear();
                exceptionParametros.AddRange(exception?.Values().ToList());
                exceptionMsgErro = exceptionParametros[0].ToString();
                exceptionMsgErro1 = exceptionParametros[1].ToString();
            }
            #endregion


            return new Retorno<string>()
            {
                MensagemErro = exceptionMsgErro
             ,
                Propriedades = !houveSucessoNaChecagem
                            ? exceptionMsgErro + " -- " + exceptionMsgErro1
                            : "Sucesso"

            ,
                Codigo = (houveSucessoNaChecagem) ? Helper.Utils.CODIGO.Sucesso : Helper.Utils.CODIGO.Erro
            };

        }

        public string ChecarFuncionalidade(string mensagem)
        {
            try
            {
                var retorno = logG.Debug(mensagem, "siscoe");
                return retorno;
            }
            catch (Exception ex1)
            {
                var retorno = "{ \"Sucesso\" : false, \"Exception\": { Message: \"" + ex1 + "\" } }";
                return retorno;
            }
        }


        public void RegistrarSomenteNlog(string mensagem)
        {
            try
            {
                System.Diagnostics.Debug.WriteLine(string.Concat("Info: ", mensagem));
            }
            catch (Exception ex1)
            {
            }
        }


        public string Info(string mensagem)
        {
            try
            {
                var retorno = logG.Info(mensagem, "siscoe");
                System.Diagnostics.Debug.WriteLine(string.Concat("Error: ", retorno));
                return retorno;
            }
            catch (Exception ex1)
            {
                return string.Concat("Error no Info log", ex1.Message);
            }
        }

        public string Info(StringBuilder mensagem)
        {
            try
            {
                var retorno = logG.Info(mensagem.ToString(), "siscoe");
                return retorno;
            }
            catch (Exception ex1)
            {
                return string.Concat("Error no Info log1", ex1.Message);
            }
        }
        #endregion

        #region Debug
        public string Debug(string mensagem)
        {
            try
            {
                var retorno = logG.Debug(mensagem, "siscoe");
                return retorno;
            }
            catch (Exception ex1)
            {
                return string.Concat("Error no Debug log", ex1.Message);
            }

        }
        public string Debug(StringBuilder mensagem)
        {
            try
            {
                var retorno = logG.Debug(mensagem.ToString(), "siscoe");
                return retorno;
            }
            catch (Exception ex1)
            {
                return string.Concat("Error no Debug log1", ex1.Message);
            }
        }
        #endregion

        #region Fatal Error

        public string Fatal(string mensagem)
        {
            try
            {
                var retorno = logG.Fatal(mensagem.ToString(), "siscoe");
                return retorno;
            }
            catch (Exception ex1)
            {
                return string.Concat("Error no Fatal log", ex1.Message);
            }
        }

        public string Fatal(string mensagem, Exception ex)
        {
            try
            {
                var retorno = logG.Fatal(string.Concat(mensagem, ex), "siscoe");
                return retorno;
            }
            catch (Exception ex1)
            {
                return string.Concat("Error no Fatal log1", ex1.Message);
            }
        }

        public string Fatal(StringBuilder mensagem, Exception ex)
        {
            try
            {
                var retorno = logG.Fatal(string.Concat(mensagem, ex), "siscoe");
                return retorno;
            }
            catch (Exception ex1)
            {
                return string.Concat("Error no Fatal log2", ex1.Message);
            }
        }
        #endregion


        #endregion



    }
}
