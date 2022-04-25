namespace Iris.Api.Controllers.LettersControllers
{
    /// <summary>
    /// Откуда получать письма
    /// </summary>
    public enum LettersFrom
    {
        /// <summary>
        /// С почтового сервера
        /// </summary>
        Server,

        /// <summary>
        /// Сохраненные в локальную базу
        /// </summary>
        LocalDb,

        /// <summary>
        /// С локальной базы и удаленного сервера
        /// </summary>
        LocalAndRemote
    }
}
