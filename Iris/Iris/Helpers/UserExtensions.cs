using Iris.Database;
using Microsoft.EntityFrameworkCore;

namespace Iris.Helpers
{
    /// <summary>
    /// Расширения для User
    /// </summary>
    public static class UserExtensions
    {
        /// <summary>
        /// Имеет ли учетную запись
        /// </summary>
        /// <param name="user">Пользователь</param>
        /// <param name="name">Имя учетной записи</param>
        public static bool HasAccount(this User user, string name)
        {
            return user.Accounts.SingleOrDefault(_ => _.Name == name) != null;
        }

        /// <summary>
        /// Получить пользователя с учетными записями
        /// </summary>
        /// <param name="users">Пользователи</param>
        /// <param name="userId">Id пользователя</param>
        public static User GetUserWithAccounts(this DbSet<User> users, int userId)
        {
            return users
                .Include(_ => _.Accounts)
                .SingleOrDefault(_ => _.Id == userId);
        }
    }
}
