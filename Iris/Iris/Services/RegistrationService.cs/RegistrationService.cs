using Iris.Api.Controllers.RegistrationControllers;
using Iris.Database;
using Iris.Services.UserService;

namespace Iris.Services.RegistrationService.cs
{
    public class RegistrationService : IRegistrationService
    {
        private readonly IUserService _userService;
        private readonly DatabaseContext _databaseContext;

        private static readonly Serilog.ILogger Log = Serilog.Log.ForContext<RegistrationService>();
        public RegistrationService(IUserService userService, DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
            _userService = userService;
        }

        public RegistrationResponseContract RegisterUser(RegistrationRequestContract contract)
        {
            bool isUserExist = _userService.GetUserByLogin(contract.Name) != null;

            if (isUserExist)
            {
                Log.Error($"User { contract.Name} is already exist");
                return new RegistrationResponseContract
                {
                    IsSucces = false,
                    Errors = new[] {$"User {contract.Name} is already exist"}
                };
            }

            _databaseContext.Users.Add(new User
            {
                Name = contract.Name,
                Password = contract.Password,
                IsAdmin = contract.IsAdmin,
                CreatedBy = contract.CreatedBy
            });

            _databaseContext.SaveChanges();

            return new RegistrationResponseContract {IsSucces = true};
        }
    }
}
