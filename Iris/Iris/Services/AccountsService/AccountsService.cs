using Iris.Api.Controllers.AccountsControllers;
using Iris.Database;
using Iris.Exceptions;
using Iris.Helpers;
using Iris.Stores.ServiceConnectionStore;

namespace Iris.Services.AccountsService
{
    /// <inheritdoc cref="IAccountsService"/>
    public class AccountsService : IAccountsService
    {
        private readonly DatabaseContext _databaseContext;
        private readonly IServerConnectionStore _connectionStore;

        /// <summary>
        /// .ctor
        /// </summary>
        public AccountsService(DatabaseContext databaseContext, IServerConnectionStore connectionStore)
        {
            _databaseContext = databaseContext;
            _connectionStore = connectionStore;
        }

        /// <inheritdoc/>
        public void AddNewAccount(int userId, AccountRequestContract contract)
        {
            var user = _databaseContext.Users.GetUserWithAccounts(userId) ?? throw new Exception();

            if (!user.HasAccount(contract.Name))
            {
                var account = new Account
                {
                    ConnectionProtocol = contract.ConnectionProtocol,
                    MailServerId = contract.MailServerId,
                    Password = contract.Password,
                    Name = contract.Name,
                    UserId = userId,
                    UseSsl = contract.UseSsl
                };

                var acc = _databaseContext.Add(account);
                _databaseContext.SaveChanges();
                _connectionStore.AddConnection(acc.Entity);
            }
            else
            {
                throw new AccountAlreadyExistException(contract.Name);
            }
        }

        /// <inheritdoc/>
        public void RemoveAccount(int userId, int accId)
        {
            _connectionStore.RemoveConnection(userId, accId);
        }
    }
}
