using Iris.Api.Controllers.RegistrationControllers;

namespace Iris.Services.RegistrationService.cs
{
    /// <summary>
    /// Сервис регистрации пользователей
    /// </summary>
    public interface IRegistrationService
    {
        /// <summary>
        /// Зарегистрировать пользователя
        /// </summary>
        /// <param name="contract">Контракт создания пользователя</param>
        /// <returns>Контракт ответа регистрации</returns>
        RegistrationResponseContract RegisterUser(RegistrationRequestContract contract);
    }
}
