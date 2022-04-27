using Iris.Api.Controllers.LettersControllers;
using Iris.Services.LettersService.Contracts;

namespace Iris.Services.LettersService
{
    /// <summary>
    /// Сервис работы с письмами
    /// </summary>
    public interface ILetterService
    {
        /// <summary>
        /// Получить все письма
        /// </summary>
        /// <param name="userId">Id пользователя</param>
        IEnumerable<LetterContract> GetAllLetters(int userId);

        /// <summary>
        /// Получить письма по запросу
        /// </summary>
        /// <param name="userId">Id пользователя</param>
        /// <param name="lettersRequest">Параметры для фильтрации, сортировки и пагинации при получении писем</param>
        IEnumerable<LetterContract> GetLetters(int userId, LettersRequest lettersRequest);
    }
}
