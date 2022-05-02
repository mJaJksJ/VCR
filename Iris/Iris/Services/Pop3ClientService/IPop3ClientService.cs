using Iris.Api.Controllers.LettersControllers;
using Iris.Services.LettersService.Contracts;
using MailKit.Net.Pop3;

namespace Iris.Services.Pop3ClientService
{
    /// <summary>
    /// Сервис для Pop3Client
    /// </summary>
    public interface IPop3ClientService
    {
        /// <summary>
        /// Получить письма
        /// </summary>
        /// <param name="pop3Client"></param>
        /// <param name="needAttachments">Получать ли вложения</param>
        /// <param name="accId">Id учетной записи</param>
        IEnumerable<LetterContract> GetLetters(Pop3Client pop3Client, NeedAttachments needAttachments, int accId);
    }
}
