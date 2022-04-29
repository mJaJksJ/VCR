using Iris.Api.Controllers.RegistrationControllers;

namespace Iris.Services.RegistrationService.cs
{
    public interface IRegistrationService
    {
        RegistrationResponseContract RegisterUser(RegistrationRequestContract contract);
    }
}
