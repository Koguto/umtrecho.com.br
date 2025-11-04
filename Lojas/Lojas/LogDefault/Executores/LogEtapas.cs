using System;
using System.Collections.Generic;

namespace LogDefault
{

    public class LogEtapas<T> where T : Dictionary<string, string>
    {

        #region Singleton

        /// <summary>
        /// Instancia do padrão singleton
        /// </summary>
        private static LogEtapas<T> instancia;

        /// <summary>
        /// Propriedade responsável por:
        ///     - Retornar uma unica instancia conforme padrão do singleton 
        ///  
        /// </summary>
        public static LogEtapas<T> Instancia
        {
            get
            {
                if (instancia == null)
                {
                    try
                    {

                        instancia = new LogEtapas<T>();
                    }
                    catch (Exception ex)
                    {
                    }
                }

                return instancia;
            }
        }


        #endregion
    }
}
