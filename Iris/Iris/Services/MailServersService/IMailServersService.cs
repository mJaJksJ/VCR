using Iris.Api.Controllers.ConnectionsControllers;
using Iris.Database;

namespace Iris.Services.MailServersService
{
    /// <summary>
    /// Сервис работы с почтовыми серверами
    /// </summary>
    public interface IMailServersService
    {
        /// <summary>
        /// Получить список аккаунтов к почтовым серверам пользователя
        /// </summary>
        /// <param name="userId">Id пользователя</param>
        IEnumerable<MailServerAccountContract> GetMailServerAccounts(int userId);

        /// <summary>
        /// Добавить новый почтовый сервер
        /// </summary>
        /// <param name="mailServerContract">Контракт добавления сервера</param>
        void NewMailServer(MailServerContract mailServerContract);

        /// <summary>
        /// Получить список доступных почтовых серверов
        /// </summary>
        /// <param name="userId">Id пользователя</param>
        IEnumerable<MailServer> GetAvailableMailServers(int userId);
    }
}
