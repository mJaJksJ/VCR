namespace Iris.Api.Controllers.RegistrationControllers
{
    /// <summary>
    /// Контракт создания пользователя
    /// </summary>
    public class RegistrationRequestContract
    {
        /// <summary>
        /// Имя
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Пароль
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Является ли администратором
        /// </summary>
        public bool IsAdmin { get; set; }

        /// <summary>
        /// Кем создан
        /// </summary>
        public int CreatedBy { get; set; }

        /// <summary>
        /// Токен
        /// </summary>
        public string Token { get; set; }
    }
}
