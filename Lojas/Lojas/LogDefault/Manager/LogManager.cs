
namespace LogDefault
{
    public class LogManager
    {
        #region Singleton

        /// <summary>
        /// Instancia do padrão singleton
        /// </summary>
        private static LogServico logServico;

        /// <summary>
        /// Propriedade responsável por:
        ///     - Retornar uma unica instancia conforme padrão do singleton 
        ///  
        /// </summary>
        public static LogServico GetCurrentClassLogger()
        {
            if (logServico == null)
            {
                logServico = new LogServico();
            }

            return logServico;
        }



        #endregion




    }
}
