using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Iris.Controllers.AuthController
{
    /// <summary>
    /// Запрос авторизации
    /// </summary>
    public class AuthRequest
    {
        /// <summary>
        /// Логин
        /// </summary>
        [Required]
        [JsonProperty("login")]
        public string Login { get; set; }

        /// <summary>
        /// Пароль
        /// </summary>
        [Required]
        [JsonProperty("password")]
        public string Password { get; set; }
    }
}
