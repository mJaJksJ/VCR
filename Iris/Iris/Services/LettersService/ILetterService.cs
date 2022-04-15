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
    }
}
