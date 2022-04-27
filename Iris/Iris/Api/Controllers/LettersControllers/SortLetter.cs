using Newtonsoft.Json;

namespace Iris.Api.Controllers.LettersControllers
{
    /// <summary>
    /// Сортировка писема
    /// </summary>
    public class SortLetter
    {
        /// <summary>
        /// Поле
        /// </summary>
        [JsonProperty("field")]
        public LetterField Field { get; set; }

        /// <summary>
        /// Способ сортировки
        /// </summary>
        [JsonProperty("sort_by")]
        public SortBy SortBy { get; set; }
    }
}
