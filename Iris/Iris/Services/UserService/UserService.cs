using Iris.Attributes;
using Iris.Database;
using Iris.Exceptions.UserExceptions;

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
        [DbGetterData]
        public void EnsureUserExist(int userId)
        public User EnsureUserExist(int userId)
        {
            var user = _databaseContext.Users.SingleOrDefault(_ => _.Id == userId);
            return user ?? throw new UserNotExistException(userId);

        }

        /// <inheritdoc/>
        [DbGetterData]
        public User GetUserByLogin(string login)
        {
            var user = _databaseContext.Users.SingleOrDefault(_ => _.Name == login);
            if (user == null)
            {
                throw new UserNotExistException(login);
            }

            return user;
        }
    }
}
