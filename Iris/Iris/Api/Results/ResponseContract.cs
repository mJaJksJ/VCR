namespace Iris.Api.Results
{
    /// <summary>
    /// Контракт ответа на запрос
    /// </summary>
    public abstract class ResponseContract
    {
        /// <summary>
        /// Ошибки
        /// </summary>
        public IEnumerable<string> Errors { get; set; }
    }
}
