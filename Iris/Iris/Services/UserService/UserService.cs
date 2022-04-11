using Iris.Database;
using Iris.Exceptions.UserExceptions;
using Iris.Stores.ServiceConnectionStore;

namespace Iris.Services.UserService
{
    /// <inheritdoc cref="IUserService"/>
    public class UserService: IUserService
    {
        private readonly DatabaseContext _databaseContext;

        private readonly IServerConnectionStore _serverCollectionStore;

        /// <summary>
        /// .ctor
        /// </summary>
        public UserService(DatabaseContext databaseContext, IServerConnectionStore serverConnectionStore)
        {
            _databaseContext = databaseContext;
            _serverCollectionStore = serverConnectionStore;
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
