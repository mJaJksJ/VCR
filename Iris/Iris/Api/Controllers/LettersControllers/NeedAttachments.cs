namespace Iris.Api.Controllers.LettersControllers
{
    /// <summary>
    /// Получать ли вложения
    /// </summary>
    public enum NeedAttachments
    {
        /// <summary>
        /// Без вложений
        /// </summary>
        WithoutAttachments,

        /// <summary>
        /// только названия вложений
        /// </summary>
        OnlyName,

        /// <summary>
        /// С вложениями
        /// </summary>
        WithAttachmentsBlob
    }
}
