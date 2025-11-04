
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LogDefault
{ 
    /// <summary>
    /// Classe responsável por efetuar as operações de CRUD do Log
    /// </summary>
    public interface ILogRegistrador<T> where T : Dictionary<string, string>
    {

        #region Arquivo
        /// <summary>
        /// Método responsável por:
        ///     - Salvar o log como "Informação"
        /// </summary>
        /// <returns>
        ///     Json contendo informaçoes sobre a execução solicitada. 
        ///     Json é a serialização da classe LoggRetorno
        /// </returns>
        string RegistrarInfo(T info, string tag_App);
        /// <summary>
        /// Método responsável por:
        ///     - Salvar o log como "Debug"
        /// </summary>
        /// <returns>
        ///     Json contendo informaçoes sobre a execução solicitada. 
        ///     Json é a serialização da classe LoggRetorno
        /// </returns>
        string RegistrarDebug(T info, string tag_App);
        /// <summary>
        /// Método responsável por:
        ///     - Salvar o log como "Alerta"
        /// </summary>
        /// <returns>
        ///     Json contendo informaçoes sobre a execução solicitada. 
        ///     Json é a serialização da classe LoggRetorno
        /// </returns>
        string RegistrarWarn(T info, string tag_App);

        /// <summary>
        /// Método responsável por:
        ///     - Salvar o log como "Error"
        /// </summary>
        /// <returns>
        ///     Json contendo informaçoes sobre a execução solicitada. 
        ///     Json é a serialização da classe LoggRetorno
        /// </returns>
        string RegistrarError(T info, string tag_App);
        #endregion

    }
}
