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
        /// Получить письма по запросу
        /// </summary>
        /// <param name="userId">Id пользователя</param>
        /// <param name="lettersRequest">Параметры для фильтрации, сортировки и пагинации при получении писем</param>
        IEnumerable<LetterContract> GetLetters(int userId, LettersRequest lettersRequest);

        /// <summary>
        ///  Установить флаг
        /// </summary>
        /// <param name="userId">Id пользователя</param>
        /// <param name="accId">Id учетной записи</param>
        /// <param name="letterId">Id письма</param>
        /// <param name="flag">Флаг</param>
        void ChangeFlag(int userId, int accId, string letterId, int flag);

        /// <summary>
        ///  Удалить письио
        /// </summary>
        /// <param name="userId">Id пользователя</param>
        /// <param name="accId">Id учетной записи</param>
        /// <param name="letterId">Id письма</param>
        void RemoveLetter(int userId, int accId, string letterId);
    }
}
