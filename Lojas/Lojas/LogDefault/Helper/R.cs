using System;
using System.Collections.Generic;
using System.Text;

namespace LogDefault
{
    public static class R
    {
        //public static class DEFAULT
        //{
        //    public static string DATA = string.Concat(DateTime.Now.Year
        //                                    , "_"
        //                                    , DateTime.Now.Month.ToString().PadLeft(2, '0')
        //                                    , "_", DateTime.Now.Day.ToString().PadLeft(2, '0')
        //                                    );
        //}
        public static class DIRETORIOS
        {
            public static readonly string PROPERTIES_FILES = @"Não definido no componente";
        }
        public static class FORMATOS
        {
            public static readonly string DATA = "{0}-{1}-{2} {3}:{4}:{5}";

            public static readonly string DELIMITADOR = "; ";
            public static readonly string END_LINE = "|";
        }
        public static class LEVEL
        {
            public const string DEFAULT = "Não informado";
            public const string INFO = "INFO";
            public const string WARNING = "WARN";
            public const string ERROR = "ERROR";
            public const string FATAL = "FATAL";
            public const string DEBUG = "DEBUG";
            public const string EVENTVIEWER = "EVENTVIEWER";

        }

        public static class FAKE
        {
            #region prop
            public static Dictionary<string, string> EVENTO_INFO_1
            {
                get
                {

                    var sb = new StringBuilder();

                    Dictionary<string, string> model = new Dictionary<string, string>
                    {
                        { "Ip", "192.168.0.02" },
                        { "Identificador", "1009/112/22" },

                        { R.CAMPOS.OBRIGATORIOS.CLASSE.NOME, "ConsolidadoController.cs" },
                        { R.CAMPOS.OBRIGATORIOS.CLASSE.METODO, "InserirUnidadesOrgao" },
                        { R.CAMPOS.OBRIGATORIOS.CLASSE.NAMESPACE, "Prodesp.TCESP.SisCOE.Consolidado" },
                    };
                    return model;
                }
            }
            public static Dictionary<string, string> EVENTO_ERRO_1
            {
                get
                {

                    var sb = new StringBuilder();

                    Dictionary<string, string> model = new Dictionary<string, string>
                    {
                        { "Ip", "192.168.0.19" },
                        { "Identificador", "1221/678/22" },

                        { R.CAMPOS.OBRIGATORIOS.MENSAGEM, sb.Append(R.FAKE.ERRO_TEXTO).ToString() },
                        { R.CAMPOS.OBRIGATORIOS.CLASSE.NOME, "SinteseBusiness.cs" },
                        { R.CAMPOS.OBRIGATORIOS.CLASSE.METODO, "SalvarSinteseConsolidado" },
                        { R.CAMPOS.OBRIGATORIOS.CLASSE.NAMESPACE, "Prodesp.TCESP.SisCOE.Sintese" },

                    };
                    return model;
                }
            }
            public static Dictionary<string, string> EVENTO_WARN_1
            {
                get
                {

                    var sb = new StringBuilder();

                    Dictionary<string, string> model = new Dictionary<string, string>
                    {
                        { "Ip", "192.168.0.22" },
                        { "Identificador", "6774/233/34" },

                        { R.CAMPOS.OBRIGATORIOS.MENSAGEM, sb.Append(R.FAKE.WARN_TEXTO).ToString() },
                        { R.CAMPOS.OBRIGATORIOS.CLASSE.NOME, "ProtocoloRepository.cs" },
                        { R.CAMPOS.OBRIGATORIOS.CLASSE.METODO, "InserirProtocolos" },
                        { R.CAMPOS.OBRIGATORIOS.CLASSE.NAMESPACE, "Prodesp.TCESP.SisCOE.Database" },
                    };
                    return model;
                }
            }
            public static Dictionary<string, string> EVENTO_FATAL_1
            {
                get
                {

                    var sb = new StringBuilder();

                    Dictionary<string, string> model = new Dictionary<string, string>
                    {
                        { "Ip", "192.168.0.67" },
                        { "Identificador", "1555/233/34" },

                        { R.CAMPOS.OBRIGATORIOS.MENSAGEM, sb.Append(R.FAKE.FATAL_TEXTO).ToString() },
                        { R.CAMPOS.OBRIGATORIOS.CLASSE.NOME, "ServicoIntegracao.cs" },
                        { R.CAMPOS.OBRIGATORIOS.CLASSE.METODO, "CalcularDespesa" },
                        { R.CAMPOS.OBRIGATORIOS.CLASSE.NAMESPACE, "Prodesp.TCESP.SisCOE.Servicos" },
                    };
                    return model;
                }
            }
            public static Dictionary<string, string> EVENTO_DEBUG_1
            {
                get
                {

                    var sb = new StringBuilder();

                    Dictionary<string, string> model = new Dictionary<string, string>
                    {
                        { R.CAMPOS.OBRIGATORIOS.MENSAGEM, sb.Append(R.FAKE.DEBUG_TEXTO).ToString() },
                        { "Ip", "205.168.0.33" },
                        { "Identificador", "0122/233/34" },
                        { "ClasseNome", "Redux.component.html" },
                        { "Namespace", "angular.cs" },
                        { "ClasseMetodo", "FactoryClasses.component.ts" }
                    };
                    return model;
                }
            }
            public static Dictionary<string, string> EVENTO_DEFAULT_1
            {
                get
                {

                    var sb = new StringBuilder();

                    Dictionary<string, string> model = new Dictionary<string, string>
                    {
                        { R.CAMPOS.OBRIGATORIOS.MENSAGEM, sb.Append(R.FAKE.DEFAULT_TEXTO).ToString() },
                        { "Ip", "000.000.0.00" },
                        { "Identificador", "0000/000/00" },
                        { "ClasseNome", "[0].html" },
                        { "Namespace", "[0].cs" },
                        { "ClasseMetodo", "[0].ts" }
                    };
                    return model;
                }
            }

            #endregion

            #region msg
            public static string INFO_TEXTO = "O sistema irá entrar em manutenção as 00h pelo período de 30 min.";
            public static string ERRO_TEXTO = @"Rastreamento de pilha:    em System.Data.SqlClient.SqlInternalConnectionTds..ctor(DbConnectionPoolIdentity identity, SqlConnectionString connectionOptions, SqlCredential credential, Object providerInfo, String newPassword, SecureString newSecurePassword, Boolean redirectedUserInstance, SqlConnectionString userConnectionOptions, SessionData reconnectSessionData, DbConnectionPool pool, String accessToken, Boolean applyTransientFaultHandling, SqlAuthenticationProviderManager sqlAuthProviderManager)
                                       em System.Data.SqlClient.SqlConnectionFactory.CreateConnection(DbConnectionOptions options, DbConnectionPoolKey poolKey, Object poolGroupProviderInfo, DbConnectionPool pool, DbConnection owningConnection, DbConnectionOptions userOptions)
                                       em System.Data.ProviderBase.DbConnectionFactory.CreatePooledConnection(DbConnectionPool pool, DbConnection owningObject, DbConnectionOptions options, DbConnectionPoolKey poolKey, DbConnectionOptions userOptions)
                                       em System.Data.ProviderBase.DbConnectionPool.CreateObject(DbConnection owningObject, DbConnectionOptions userOptions, DbConnectionInternal oldConnection)
                                       em System.Data.ProviderBase.DbConnectionPool.UserCreateRequest(DbConnection owningObject, DbConnectionOptions userOptions, DbConnectionInternal oldConnection)
                                       em System.Data.ProviderBase.DbConnectionPool.TryGetConnection(DbConnection owningObject, UInt32 waitForMultipleObjectsTimeout, Boolean allowCreate, Boolean onlyOneCheckConnection, DbConnectionOptions userOptions, DbConnectionInternal& connection)
                                       em System.Data.ProviderBase.DbConnectionPool.TryGetConnection(DbConnection owningObject, TaskCompletionSource`1 retry, DbConnectionOptions userOptions, DbConnectionInternal& connection)
                                       em System.Data.ProviderBase.DbConnectionFactory.TryGetConnection(DbConnection owningConnection, TaskCompletionSource`1 retry, DbConnectionOptions userOptions, DbConnectionInternal oldConnection, DbConnectionInternal& connection)
                                       em System.Data.ProviderBase.DbConnectionInternal.TryOpenConnectionInternal(DbConnection outerConnection, DbConnectionFactory connectionFactory, TaskCompletionSource`1 retry, DbConnectionOptions userOptions)
                                       em System.Data.ProviderBase.DbConnectionClosed.TryOpenConnection(DbConnection outerConnection, DbConnectionFactory connectionFactory, TaskCompletionSource`1 retry, DbConnectionOptions userOptions)
                                       em System.Data.SqlClient.SqlConnection.TryOpenInner(TaskCompletionSource`1 retry)
                                       em System.Data.SqlClient.SqlConnection.TryOpen(TaskCompletionSource`1 retry)
                                       em System.Data.SqlClient.SqlConnection.Open()
                                       em Prodesp.Framework.Dados.Dados..ctor(Catalogo catalogo, BancoDados bancoDados, String connectionString, Int32 commandTimeout)
                                       em Prodesp.Framework.Dados.Dados..ctor(Catalogo catalogo, BancoDados bancoDados)
                                       em Prodesp.TCESP.SisCOE.Repository.ParametroConfiguracao.Obter(Int32 idParametroConfiguracao, String chave) na C:\projetos\siscoe_angular\SisCOE\Prodesp.TCESP.SisCOE\Prodesp.TCESP.SisCOE.Repository\ParametroConfiguracao.cs:linha 81
                                       em Prodesp.TCESP.SisCOE.Repository.ParametroConfiguracao.ObterTodos() ";
            public static string WARN_TEXTO = "Usar senha sem criptografica é inseguro";
            public static string FATAL_TEXTO = "Banco de dados não está comunicável";
            public static string DEBUG_TEXTO = "Passei por aqui, linha 2";
            public static string DEFAULT_TEXTO = "Nenhum evento definido";
            #endregion
        }

        public class EVENT_VIEWER
        {
            public static readonly string SOURCE = "LoggService";
            public static readonly string LOG_NAME = "Application";
        }

        public class CAMPOS
        {
            public static class OBRIGATORIOS
            {

                public const string MENSAGEM = "Mensagem";

                public class CLASSE
                {
                    public const string NOME = "ClasseNome";
                    public const string METODO = "ClasseMetodo";
                    public const string NAMESPACE = "ClasseNamespace";
                }

            }

            public static class SISTEMA
            {
                public const string DT_HORA = "DataHora";
                public const string LEVEL = "Level";
                public const string FLAG_INFO = "FlgInfo";
            }

        }


    }
}
