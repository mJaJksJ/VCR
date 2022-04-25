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
        public LetterField Field { get; set; }

        /// <summary>
        /// Шаблон. Кроме Text и Attacments!
        /// </summary>
        public string Template { get; set; }

        /// <summary>
        /// Шаблоны. Только для Text и Attacments!
        /// </summary>
        public IEnumerable<string> Templates { get; set; }

        /// <summary>
        /// Является ли фильтр регулярным выражением
        /// </summary>
        public bool IsRegex { get; set; }
    }
}
