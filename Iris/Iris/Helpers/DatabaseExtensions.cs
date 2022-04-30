using Iris.Database;
using Microsoft.EntityFrameworkCore;

namespace Iris.Helpers
{
    public static class DatabaseExtensions
    {

        /// <summary>
        /// Получить пользователя с учетными записями
        /// </summary>
        /// <param name="users">Пользователи</param>
        /// <param name="userId">Id пользователя</param>
        public static User GetUserWithAccounts(this DatabaseContext db, int userId)
        {
            return db.Users
                .Include(_ => _.Accounts)
                .SingleOrDefault(_ => _.Id == userId);
        }
    }
}
