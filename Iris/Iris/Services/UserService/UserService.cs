using Iris.Database;
using Iris.Exceptions.UserExceptions;
using Iris.Helpers.DatabaseExtensions;

namespace Iris.Services.UserService
{
    /// <inheritdoc cref="IUserService"/>
    public class UserService : IUserService
    {
        private readonly DatabaseContext _databaseContext;

        /// <summary>
        /// .ctor
        /// </summary>
        public UserService(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        /// <inheritdoc/>
        public void EnsureUserExist(int userId)
        {
            var user = _databaseContext.GetUserById(userId);
            if (user == null)
            {
                throw new UserNotExistException(userId);
            }
        }

        /// <inheritdoc/>
        public User GetUserByLogin(string login)
        {
            var user = _databaseContext.GetUserByLogin(login);
            if (user == null)
            {
                throw new UserNotExistException(login);
            }

            return user;
        }
    }
}
