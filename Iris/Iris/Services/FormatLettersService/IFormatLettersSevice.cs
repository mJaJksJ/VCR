using Iris.Enums;
using Iris.Services.LettersService.Contracts;

namespace Iris.Services.FormatLettersService
{
    /// <summary>
    /// Сервис форматирования писем в xml/json
    /// </summary>
    public interface IFormatLettersSevice
    {
        /// <summary>
        /// Получение формата из его строкового обозначения
        /// </summary>
        /// <param name="format">Название формата в виде строки (xml/json)</param>
        ResponseFormat GetFormat(string format);

        /// <summary>
        /// Получить письмо в нужном формате
        /// </summary>
        /// <param name="letterContract">Контракт письма</param>
        /// <param name="responseFormat">Формат</param>
        string FormatLetter(LetterContract letterContract, ResponseFormat responseFormat);

        /// <summary>
        /// Получить письма в нужном формате
        /// </summary>
        /// <param name="letterContracts">Контракты писем</param>
        /// <param name="responseFormat">Формат</param>
        string FormatLetters(IEnumerable<LetterContract> letterContracts, ResponseFormat responseFormat);
    }
}
