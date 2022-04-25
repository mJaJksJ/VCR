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
        public Dictionary<int, Settings> AccountsSettings { get; set; }

        /// <summary>
        /// Глобальные настройки получения писем
        /// </summary>
        public Settings GlobalSettings { get; set; }

        /// <summary>
        /// Сохранять ли письма в локальную базу данных
        /// </summary>
        public bool SaveLettersToLocalBd { get; set; }

        /// <summary>
        /// Фильтры писем
        /// </summary>
        public IEnumerable<FilterLetter> Filters { get; set; }

        /// <summary>
        /// Сортировка писема
        /// </summary>
        public IEnumerable<SortLetter> Sorts { get; set; }
    }
}
