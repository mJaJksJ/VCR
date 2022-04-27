using Newtonsoft.Json;

namespace Iris.Api.Controllers.LettersControllers
{
    /// <summary>
    /// Фильтры писем
    /// </summary>
    public class FilterLetter
    {
        /// <summary>
        /// Поля контракта письма
        /// </summary>
        [JsonProperty("field")]
        public LetterField Field { get; set; }

        /// <summary>
        /// Шаблон. Кроме Text и Attacments!
        /// </summary>
        [JsonProperty("template")]
        public string Template { get; set; }

        /// <summary>
        /// Шаблоны. Только для Text и Attacments!
        /// </summary>
        [JsonProperty("templates")]
        public IEnumerable<string> Templates { get; set; }

        /// <summary>
        /// Является ли фильтр регулярным выражением
        /// </summary>
        [JsonProperty("is_regex")]
        public bool IsRegex { get; set; }

        /// <summary>
        /// .ctor
        /// </summary>
        public FilterLetter()
        {
            Templates = new List<string>();
        }
    }
}
