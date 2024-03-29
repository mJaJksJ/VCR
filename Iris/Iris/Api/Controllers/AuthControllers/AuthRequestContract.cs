﻿using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Iris.Api.Controllers.AuthControllers
{
    /// <summary>
    /// Контракт запроса авторизации
    /// </summary>
    public class AuthRequestContract
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

        /// <summary>
        /// Ключ-пароль
        /// </summary>
        [Required]
        [JsonProperty("key_password")]
        public string KeyPassword { get; set; }
    }
}
