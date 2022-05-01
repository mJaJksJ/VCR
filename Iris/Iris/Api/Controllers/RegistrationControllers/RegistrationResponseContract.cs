using Iris.Api.Results;

namespace Iris.Api.Controllers.RegistrationControllers
{
    /// <summary>
    /// Контракт ответа регистрации
    /// </summary>
    public class RegistrationResponseContract : ResponseContract
    {
        /// <summary>
        /// Успешно ли проведена регистрация
        /// </summary>
        public bool IsSucces { get; set; }

        /// <summary>
        /// Секретный ключ
        /// </summary>
        public string Token { get; set; }
    }
}
