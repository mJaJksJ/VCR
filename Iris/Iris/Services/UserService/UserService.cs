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
        public void EnsureUserExist(int userId)
        {
            var user = _databaseContext.Users.SingleOrDefault(_ => _.Id == userId);
            if (user == null)
            {
                throw new UserNotExistException(userId);
            }
        }
    }
}
