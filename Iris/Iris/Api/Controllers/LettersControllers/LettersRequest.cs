using Newtonsoft.Json;

namespace Iris.Api.Controllers.LettersControllers
{
    /// <summary>
    /// Параметры для фильтрации, сортировки и пагинации при получении писем
    /// </summary>
    public class LettersRequest
    {
        /// <summary>
        /// Настройки получения писем для отдельных учетных записей
        /// </summary>
        [JsonProperty("accounts_setting")]
        public Dictionary<int, Settings> AccountsSettings { get; set; }

        /// <summary>
        /// Глобальные настройки получения писем
        /// </summary>
        [JsonProperty("global_setting")]
        public Settings GlobalSettings { get; set; }

        /// <summary>
        /// Сохранять ли письма в локальную базу данных
        /// </summary>
        [JsonProperty("save_to_db")]
        public bool SaveLettersToLocalBd { get; set; }

        /// <summary>
        /// Фильтры писем
        /// </summary>
        [JsonProperty("filters")]
        public IEnumerable<FilterLetter> Filters { get; set; }

        /// <summary>
        /// Сортировка писема
        /// </summary>
        [JsonProperty("sorts")]
        public IEnumerable<SortLetter> Sorts { get; set; }

        /// <summary>
        /// .ctor
        /// </summary>
        public LettersRequest()
        {
            AccountsSettings = new Dictionary<int, Settings>();
            Filters = new List<FilterLetter>();
            Sorts = new List<SortLetter>();
            GlobalSettings = new Settings();
        }
    }
}
