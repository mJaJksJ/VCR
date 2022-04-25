namespace Iris.Api.Controllers.LettersControllers
{
    /// <summary>
    /// Настройки получения писем
    /// </summary>
    public class Settings
    {
        /// <summary>
        /// Откуда получать письма
        /// </summary>
        public LettersFrom LettersStorage { get; set; }

        /// <summary>
        /// Получать ли вложения
        /// </summary>
        public NeedAttachments NeedAttachments { get; set; }
    }
}
