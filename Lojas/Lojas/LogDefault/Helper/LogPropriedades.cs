using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace LogDefault
{
    public class LogPropriedades
    {

        #region Singleton

        /// <summary>
        /// Instancia do padrão singleton
        /// </summary>
        public static LogPropriedades instancia;
        /// <summary>
        /// Propriedade responsável por:
        ///     - Retornar uma unica instancia conforme padrão do singleton 
        ///  
        /// </summary>
        /// 
        public static List<LogConfiguracoesArquivo> ConfiguracoesDoArquivo
        {
            get
            {
                if (instancia == null)
                {
                    Configs = new List<LogConfiguracoesArquivo>();
                    try
                    {
                        string configDiretorioAcima = ObterLocalDosArquivosDeConfig1DiretorioAcima();
                        string configMesmoDiretorio = ObterLocalDosArquivosDeConfigMesmoDiretorio();
                        CarregarPonteirosParaPropriedadesDaAplicacao(configDiretorioAcima, configMesmoDiretorio);

                    }
                    catch (Exception ex)
                    {
                        throw;
                    }
                    ///Todo: subir as properties em memória
                    return Configs;
                }

                return Configs;
            }
        }

        /// <summary>
        /// Método responsável por:
        ///     Obter o "[path]" dos arquivos de config:
        ///         - logg.properties.[appname].json
        ///     Método considera: 1 diretorio acima 
        ///     (motivo da publicação ficar em uma pasta "bin" abaixo da config)
        /// </summary>
        /// <returns>
        ///     Local do arquivo de config, com 1 diretorio acima
        /// </returns>
        private static string ObterLocalDosArquivosDeConfig1DiretorioAcima()
        {
            System.Diagnostics.Debug.WriteLine(string.Concat(
                 "Assembly: ", Assembly.GetExecutingAssembly().GetName())
                 );
            var assemblyDiretorioOrigem = Assembly.GetExecutingAssembly().GetName().CodeBase.ToString();
            var assemblyDiretorioOrigemSemProtocolo = assemblyDiretorioOrigem.Replace("file:///", "");
            var assemblyBinLocation = Directory.GetParent(assemblyDiretorioOrigemSemProtocolo).FullName;
            var assemblyRaizBinLocation = Directory.GetParent(assemblyBinLocation).FullName;
            return assemblyRaizBinLocation;
        }

        /// <summary>
        /// Método responsável por:
        ///     Obter o "[path]" dos arquivos de config:
        ///         - logg.properties.[appname].json
        ///     Método considera: mesmo diretorio 
        ///     (motivo da publicação ficar na mesma pasta dos config - ex. WinService, .netCore)
        /// </summary>
        /// <returns>
        ///     Local do arquivo de config, no mesmo diretorio
        /// </returns>
        private static string ObterLocalDosArquivosDeConfigMesmoDiretorio()
        {
            System.Diagnostics.Debug.WriteLine(string.Concat(
                 "Assembly: ", Assembly.GetExecutingAssembly().GetName())
                 );
            var assemblyDiretorioOrigem = Assembly.GetExecutingAssembly().GetName().CodeBase.ToString();
            var assemblyDiretorioOrigemSemProtocolo = assemblyDiretorioOrigem.Replace("file:///", "");
            var assemblyLocation = Directory.GetParent(assemblyDiretorioOrigemSemProtocolo).FullName;
            return assemblyLocation;
        }






        #endregion


        private static List<LogConfiguracoesArquivo> Configs;
        private static string instanciaLocal1DiretorioAcimaConfigs;
        private static string instanciaLocalMesmoDiretorioConfigs;
        public static string[] LOCALIZACAO_ARQUIVOS_CONFIG
        {

            get
            {
                if (instanciaLocal1DiretorioAcimaConfigs == null)
                {
                    instanciaLocal1DiretorioAcimaConfigs = ObterLocalDosArquivosDeConfig1DiretorioAcima();
                }

                if (instanciaLocalMesmoDiretorioConfigs == null)
                {
                    instanciaLocalMesmoDiretorioConfigs = ObterLocalDosArquivosDeConfigMesmoDiretorio();
                }

                var locaisConfig = new string[]
                {
                    instanciaLocal1DiretorioAcimaConfigs
                    , instanciaLocalMesmoDiretorioConfigs
                };

                return locaisConfig;
            }
        }

        private static void CarregarPonteirosParaPropriedadesDaAplicacao(
            string diretorioAcima
            , string mesmoDiretorio
            )
        {
            //var props = GetAllTxt();
            #region logg Json properties: Localização 1
            var props = ObterOsCaminhosDoDiretorioDoConfigJson(diretorioAcima);
            #endregion

            if (props == null || props.Count() == 0)
            {
                #region logg Json properties: Localização 2
                props = ObterOsCaminhosDoDiretorioDoConfigJson(mesmoDiretorio);
                #endregion
            }

            try
            {
                //LerStreamDoAssembly(props);
                LerOsJsonDoCaminhoUsandoStream(props);
            }
            catch (Exception ex)
            {

                throw;
            }
            //foreach (var nome in props)
            //{
            //    StreamReader r = new StreamReader(nome);
            //    string jsonString = r.ReadToEnd();
            //    LoggContrato contrato = JsonConvert.DeserializeObject<LoggContrato>(jsonString);
            //    contratos.Add(contrato);
            //}

        }

        private static void LerStreamDoAssembly(string[] resourcesAssemblyNames)
        {
            foreach (var resourceAssemblyName in resourcesAssemblyNames)
            {

                var executingAssembly = Assembly.GetExecutingAssembly();

                using (Stream stream = executingAssembly.GetManifestResourceStream(resourceAssemblyName))
                using (StreamReader reader = new StreamReader(stream))
                {
                    string jsonString = reader.ReadToEnd();
                    LogConfiguracoesArquivo contrato = JsonConvert.DeserializeObject<LogConfiguracoesArquivo>(jsonString);
                    Configs.Add(contrato);
                }

            }
        }

        private static void LerOsJsonDoCaminhoUsandoStream(string[] caminhoDoArquivoJson)
        {
            foreach (var caminho in caminhoDoArquivoJson)
            {
                var arquivoJson = File.ReadAllText(caminho);
                LogConfiguracoesArquivo contrato = JsonConvert.DeserializeObject<LogConfiguracoesArquivo>(arquivoJson);
                Configs.Add(contrato);

            }
        }



        private static string[] ObterOsCaminhosDoDiretorioDoConfigJson(string localizacao)
        {
            List<string> arquivos = new List<string>();
            foreach (var file in Directory.GetFiles(localizacao + "/", "logg.properties.*.json"))
            {
                arquivos.Add(file);
            }
            return arquivos.ToArray();
        }





        //private static string[] GetAllTxt()
        //{
        //    var executingAssembly = Assembly.GetExecutingAssembly();
        //    string folderName = string.Format("{0}.resources.properties", executingAssembly.GetName().Name);
        //    return executingAssembly
        //        .GetManifestResourceNames()
        //        .Where(r => r.StartsWith(folderName) && r.EndsWith(".json"))
        //        //.Select(r => r.Substring(folderName.Length + 1))
        //        .ToArray();
        //}

    }
}
