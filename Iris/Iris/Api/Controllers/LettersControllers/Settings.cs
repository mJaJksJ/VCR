using Newtonsoft.Json;

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
        [JsonProperty("storage")]
        public LettersFrom LettersStorage { get; set; }

        /// <summary>
        /// Получать ли вложения
        /// </summary>
        [JsonProperty("need_attachs")]
        public NeedAttachments NeedAttachments { get; set; }
    }
}
