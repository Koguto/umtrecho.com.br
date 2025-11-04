using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;

namespace LogDefault
{
    public class LogRegistrador<T> : ILogRegistrador<T> where T : Dictionary<string, string>
    {
        #region Singleton

        /// <summary>
        /// Instancia do padrão singleton
        /// </summary>
        public static LogRegistrador<T> instancia;
        /// <summary>
        /// Propriedade responsável por:
        ///     - Retornar uma unica instancia conforme padrão do singleton 
        ///  
        /// </summary>
        public static LogRegistrador<T> Instancia
        {
            get
            {
                if (instancia == null)
                {
                    instancia = new LogRegistrador<T>();
                }

                return instancia;
            }
        }




        #endregion

        #region comportamentos


        #region Local: [Arquivo]
        public string RegistrarDebug(T dic, string tagApp)
        {
            try
            {
                ValidarDic(dic);
                dic.Add(R.CAMPOS.SISTEMA.DT_HORA, DateTime.Now.ToString(CultureInfo.CurrentCulture));
                dic.Add(R.CAMPOS.SISTEMA.LEVEL, R.LEVEL.DEBUG);
                return PersistirLinhaFlatFile(dic, tagApp);
            }
            catch (LogJsonPropriedadesInvalidoException loe)
            {
                var jsonRetorno = LogRetornoPadroesDefinidos.Erro(loe);
                return jsonRetorno;
            }
            catch (LogObrigatoriedadeException loe)
            {
                var jsonRetorno = LogRetornoPadroesDefinidos.Erro(loe);
                return jsonRetorno;
            }
            catch (Exception ex)
            {
                var jsonRetorno = LogRetornoPadroesDefinidos.Erro(ex);
                return jsonRetorno;
            }
        }

        public string RegistrarError(T dic, string tagApp)
        {
            try
            {
                ValidarDic(dic);
                dic.Add(R.CAMPOS.SISTEMA.DT_HORA, DateTime.Now.ToString(CultureInfo.CurrentCulture));
                dic.Add(R.CAMPOS.SISTEMA.LEVEL, R.LEVEL.ERROR);
                return PersistirLinhaFlatFile(dic, tagApp);
            }
            catch (LogJsonPropriedadesInvalidoException loe)
            {
                var jsonRetorno = LogRetornoPadroesDefinidos.Erro(loe);
                return jsonRetorno;
            }
            catch (LogObrigatoriedadeException loe)
            {
                var jsonRetorno = LogRetornoPadroesDefinidos.Erro(loe);
                return jsonRetorno;
            }
            catch (Exception ex)
            {
                var jsonRetorno = LogRetornoPadroesDefinidos.Erro(ex);
                return jsonRetorno;
            }
        }

        public string RegistrarInfo(T dic, string tagApp)
        {
            try
            {
                ValidarDic(dic);
                dic.Add(R.CAMPOS.SISTEMA.DT_HORA, DateTime.Now.ToString(CultureInfo.CurrentCulture));
                dic.Add(R.CAMPOS.SISTEMA.LEVEL, R.LEVEL.INFO);
                return PersistirLinhaFlatFile(dic, tagApp);
            }
            catch (LogJsonPropriedadesInvalidoException loe)
            {
                var jsonRetorno = LogRetornoPadroesDefinidos.Erro(loe);
                return jsonRetorno;
            }
            catch (LogObrigatoriedadeException loe)
            {
                var jsonRetorno = LogRetornoPadroesDefinidos.Erro(loe);
                return jsonRetorno;
            }
            catch (Exception ex)
            {
                var jsonRetorno = LogRetornoPadroesDefinidos.Erro(ex);
                return jsonRetorno;
            }
        }

        public string RegistrarWarn(T dic, string tagApp)
        {
            try
            {
                ValidarDic(dic);
                dic.Add(R.CAMPOS.SISTEMA.DT_HORA, DateTime.Now.ToString(CultureInfo.CurrentCulture));
                dic.Add(R.CAMPOS.SISTEMA.LEVEL, R.LEVEL.WARNING);
                return PersistirLinhaFlatFile(dic, tagApp);
            }
            catch (LogJsonPropriedadesInvalidoException loe)
            {
                var jsonRetorno = LogRetornoPadroesDefinidos.Erro(loe);
                return jsonRetorno;
            }
            catch (LogObrigatoriedadeException loe)
            {
                var jsonRetorno = LogRetornoPadroesDefinidos.Erro(loe);
                return jsonRetorno;
            }
            catch (Exception ex)
            {
                var jsonRetorno = LogRetornoPadroesDefinidos.Erro(ex);
                return jsonRetorno;
            }
        }

        public string RegistrarFatal(T dic, string tagApp)
        {
            try
            {
                ValidarDic(dic);
                dic.Add(R.CAMPOS.SISTEMA.DT_HORA, DateTime.Now.ToString(CultureInfo.CurrentCulture));
                dic.Add(R.CAMPOS.SISTEMA.LEVEL, R.LEVEL.FATAL);
                return PersistirLinhaFlatFile(dic, tagApp);
            }
            catch (LogJsonPropriedadesInvalidoException loe)
            {
                var jsonRetorno = LogRetornoPadroesDefinidos.Erro(loe);
                return jsonRetorno;
            }
            catch (LogObrigatoriedadeException loe)
            {
                var jsonRetorno = LogRetornoPadroesDefinidos.Erro(loe);
                return jsonRetorno;
            }
            catch (Exception ex)
            {
                var jsonRetorno = LogRetornoPadroesDefinidos.Erro(ex);
                return jsonRetorno;
            }
        }

        public string RegistrarDefault(T dic, string tagApp)
        {
            try
            {
                ValidarDic(dic);
                dic.Add(R.CAMPOS.SISTEMA.DT_HORA, DateTime.Now.ToString(CultureInfo.CurrentCulture));
                dic.Add(R.CAMPOS.SISTEMA.LEVEL, R.LEVEL.DEFAULT);
                return PersistirLinhaFlatFile(dic, tagApp);
            }
            catch (LogJsonPropriedadesInvalidoException loe)
            {
                var jsonRetorno = LogRetornoPadroesDefinidos.Erro(loe);
                return jsonRetorno;
            }
            catch (LogObrigatoriedadeException loe)
            {
                var jsonRetorno = LogRetornoPadroesDefinidos.Erro(loe);
                return jsonRetorno;
            }
            catch (Exception ex)
            {
                var jsonRetorno = LogRetornoPadroesDefinidos.Erro(ex);
                return jsonRetorno;
            }
        }

        private static string PersistirLinhaFlatFile(T dic, string tagApp)
        {
            LogConfiguracoesArquivo appPropriedades = null;
            try
            {
                //DBMO: Para Debug
                //DebugRastreamento();

                foreach (var item in LogPropriedades.ConfiguracoesDoArquivo)
                {
                    if (item?.Sistema?.Nome == tagApp)
                    {
                        appPropriedades = item;
                    };
                }

            }
            catch (Exception ex)
            {
                var jsonRetorno = LogRetornoPadroesDefinidos.Erro(ex);
                return jsonRetorno;
            }


            if (appPropriedades == null)
            {
                var jsonRetorno = LogRetornoPadroesDefinidos.Erro(new LogJsonPropriedadesInvalidoException(
                    LogJsonPropriedadesInvalidoException.Mensagem));
                return jsonRetorno;
            }
            return LogExecutor<T>.Instancia.Executar(dic, appPropriedades);
        }

        private static void DebugRastreamento()
        {
            System.Diagnostics.Debug.WriteLine(string.Concat("Assembly: ", Assembly.GetExecutingAssembly().GetName()));
            var assemblyDiretorioOrigem = Assembly.GetExecutingAssembly().GetName().CodeBase.ToString();
            var assemblyDiretorioOrigemSemProtocolo = assemblyDiretorioOrigem.Replace("file:///", "");
            var assemblyBinLocation = Directory.GetParent(assemblyDiretorioOrigemSemProtocolo).FullName;
            var configDiretorioAcima = Directory.GetParent(assemblyBinLocation).FullName;


            System.Diagnostics.Debug.WriteLine(string.Concat(
            "Assembly: ", Assembly.GetExecutingAssembly().GetName())
            );
            var assemblyDiretorioOrigem2 = Assembly.GetExecutingAssembly().GetName().CodeBase.ToString();
            var assemblyDiretorioOrigemSemProtocolo2 = assemblyDiretorioOrigem2.Replace("file:///", "");
            var configMesmoDiretorio = Directory.GetParent(assemblyDiretorioOrigemSemProtocolo2).FullName;


            List<string> arquivos = new List<string>();
            foreach (var file in Directory.GetFiles(configDiretorioAcima + "/", "Log.properties.*.json"))
            {
                arquivos.Add(file);
            }


            List<string> arquivos2 = new List<string>();
            foreach (var file in Directory.GetFiles(configMesmoDiretorio + "/", "Log.properties.*.json"))
            {
                arquivos2.Add(file);
            }

            foreach (var arquivo in arquivos)
            {
                var arquivoJson = File.ReadAllText(arquivo);
                LogConfiguracoesArquivo contrato = JsonConvert.DeserializeObject<LogConfiguracoesArquivo>(arquivoJson);
            }

            foreach (var arquivo in arquivos2)
            {
                var arquivoJson = File.ReadAllText(arquivo);
                LogConfiguracoesArquivo contrato = JsonConvert.DeserializeObject<LogConfiguracoesArquivo>(arquivoJson);
            }
        }


        #endregion




        #endregion

        #region Helpers

        private void ValidarDic(T dic)
        {
            var existeMensagem = dic.ContainsKey(R.CAMPOS.OBRIGATORIOS.MENSAGEM);
            if (!existeMensagem)
            {
                throw new LogObrigatoriedadeException(LogObrigatoriedadeException.Mensagem);
            }
        }

        #endregion


    }
}
