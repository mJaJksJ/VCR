using Iris.Database;

namespace Iris.Helpers.DatabaseExtensions
{
    public static class UserExtensions
    {
        public static User GetUserByLogin(this DatabaseContext db, string login)
        {
            return db.Users.SingleOrDefault(_ => _.Name == login);
        }

        public static User GetUserById(this DatabaseContext db, int userId)
        {
            return db.Users.SingleOrDefault(_ => _.Id == userId);
        }
    }
}
