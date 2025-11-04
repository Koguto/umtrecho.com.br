using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace LogDefault
{

    public class LogExecutor<T> where T : Dictionary<string, string>
    {
        #region propriedades


        #region Singleton

        /// <summary>
        /// Instancia do padrão singleton
        /// </summary>
        private static LogExecutor<T> instancia;

        /// <summary>
        /// Propriedade responsável por:
        ///     - Retornar uma unica instancia conforme padrão do singleton 
        ///  
        /// </summary>
        public static LogExecutor<T> Instancia
        {
            get
            {
                if (instancia == null)
                {
                    try
                    {

                        instancia = new LogExecutor<T>();
                        status = 1;
                    }
                    catch (Exception ex)
                    {
                        ///log não subiu
                        ///DBMo: implementar registro do motivo em um arquivo/pasta
                        status = -1;
                    }
                }

                return instancia;
            }
        }


        #endregion


        ///
        /// Atributo responsável por:
        /// ser apenas informativo de status, não manipulável
        ///  0 - Falha
        ///  1 - Sucesso
        /// -1 - Erro
        private static int status;
        #endregion

        #region construtor
        /// <summary>
        /// construtor seguindo os padrões de singleton
        /// </summary>
        private LogExecutor()
        {

        }
        #endregion

        ///etapa 1: criar arquivo pelo padrão
        ///  [sistema|nome][data].[severidade].[extensao]
        ///          {INFO,FATAL,DEBUG.WARN}
        ///          [0].INFO.[1]
        ///          [0].FATAL.[1]
        ///          [0].DEBUG.WARN.[1]
        public string Executar(T dic, LogConfiguracoesArquivo contrato)
        {
            try
            {
                var ambienteAtual = contrato.Diretorios.Desenvolvimento;
                string formato = CriarArquivoPeloFormatoDoConfig(dic, contrato);
                if (formato == string.Empty)
                {
                    var jsonRetorno = LogRetornoPadroesDefinidos
                        .Sucesso(string.Concat("Nível definido pelo usuário para não visualização."));
                    return jsonRetorno;
                }


                switch (contrato.Diretorios.Ambiente?.ToLower())
                {
                    case "producao":
                        ambienteAtual = contrato.Diretorios.Producao;
                        break;
                    case "homologacao":
                        ambienteAtual = contrato.Diretorios.Homologacao;
                        break;
                    case "desenvolvimento":
                        ambienteAtual = contrato.Diretorios.Desenvolvimento;
                        break;
                    case "teste":
                        ambienteAtual = contrato.Diretorios.Desenvolvimento;
                        break;
                    case "padrao":
                        ambienteAtual = contrato.Diretorios.Padrao;
                        break;
                }

                var definicaoDoLocalASerSalvo = string.Concat(ambienteAtual, formato);
                return SalvarLinhaDeLog(dic, definicaoDoLocalASerSalvo);

            }
            catch (Exception ex)
            {
                var jsonRetorno = LogRetornoPadroesDefinidos.Erro(ex);
                return jsonRetorno;
            }
        }

        private static string SalvarLinhaDeLog(T dic, string definicaoDoLocalASerSalvo)
        {
            using (StreamWriter sw = File.AppendText(definicaoDoLocalASerSalvo))
            {
                //etapa 3: salvar no arquivo criado de acordo com a severidade
                var linha = ObterRegistro(dic);
                sw.WriteLine(linha);

                //etapa 4: trabalhar retorno do logg
                var jsonRetorno = LogRetornoPadroesDefinidos
                    .Sucesso(
                    string.Concat("Linha de log executada com sucesso: "
                    , definicaoDoLocalASerSalvo));

                return jsonRetorno;
            }
        }

        private string CriarArquivoPeloFormatoDoConfig(T dic, LogConfiguracoesArquivo contrato)
        {
            var grupos = contrato.Arquivo.Agrupamento.Split(',').ToList();
            var level = dic[R.CAMPOS.SISTEMA.LEVEL];

            string nomeDoGrupoASerUsado = null;
            foreach (var grupo in grupos)
            {
                if (grupo.Contains(level))
                {
                    nomeDoGrupoASerUsado = grupo;
                }
            }

            if (nomeDoGrupoASerUsado == null)
            {
                //Nao registra
                return string.Empty;
            }

            #region data

            var ano = DateTime.Now.Year;
            var mes = DateTime.Now.Month.ToString().PadLeft(2, '0');
            var dia = DateTime.Now.Day.ToString().PadLeft(2, '0');
            var minuto = DateTime.Now.Minute.ToString().PadLeft(2, '0');

            var data = string.Concat(ano, "_", mes, "_", dia);
            //var data = string.Concat(ano, "_", mes, "_", dia, "_", minuto);

            #endregion


            #region nome arquivo
            string formato = string.Concat(
                                          contrato.Sistema.Nome
                                        , "."
                                        , data
                                        , "."
                                        , nomeDoGrupoASerUsado
                                        , contrato.Arquivo.Extensao);
            #endregion
            return formato;
        }

        private static string ObterRegistro(T dic)
        {
            var registro = string.Empty;
            if (dic.Count == 0) return registro;

            foreach (var prop in dic)
            {
                registro += string.Concat("; ", prop.Key, ": ", prop.Value);
            }
            registro = registro.Remove(0, 1).TrimStart();
            return registro;
        }

    }
}
