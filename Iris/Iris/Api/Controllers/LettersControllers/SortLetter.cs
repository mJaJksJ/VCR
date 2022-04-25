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
        public LetterField Field { get; set; }

        /// <summary>
        /// Способ сортировки
        /// </summary>
        public SortBy SortBy { get; set; }
    }
}
