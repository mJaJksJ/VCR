using Newtonsoft.Json;

namespace Iris.Api.Controllers.AuthController
{
    /// <summary>
    /// Контракт ответа авторизации
    /// </summary>
    public class AuthResponseContract
    {
        /// <summary>
        /// Id пользователя
        /// </summary>
        [JsonProperty("userid")]
        public string UserId { get; set; }

        /// <summary>
        /// Логин
        /// </summary>
        [JsonProperty("login")]
        public string Login { get; set; }

        /// <summary>
        /// Роль
        /// </summary>
        [JsonProperty("roles")]
        public IEnumerable<string> Roles { get; set; }

        /// <summary>
        /// Токен
        /// </summary>
        [JsonProperty("token")]
        public string Token { get; set; }

        /// <summary>
        /// Тип токена
        /// </summary>
        [JsonProperty("token_type")]
        public string TokenType { get; set; }
    }
}
