using Iris.Api.Controllers.AccountsControllers;

namespace Iris.Services.AccountsService
{
    /// <summary>
    /// Сервис работы с учетными записями
    /// </summary>
    public interface IAccountsService
    {
        /// <summary>
        /// Добавить новую учетную запись
        /// </summary>
        /// <param name="userId">Id пользователя</param>
        /// <param name="contract">Запрос добавления новой учетной записи</param>
        void AddNewAccount(int userId, AccountRequestContract contract);

        /// <summary>
        /// Удалить учетную запись
        /// </summary>
        /// <param name="userId">Id пользователя</param>
        /// <param name="accId">Id учетной записи</param>
        void RemoveAccount(int userId, int accId);
    }
}
