using Microsoft.AspNetCore.Mvc;

namespace Iris.Api.Controllers.BaseControllers
{
    /// <summary>
    /// Функционал Pop3
    /// </summary>
    public class Pop3Controller : Controller
    {
        /// <summary>
        /// загрузка писем на устройство пользователя
        /// </summary>
        /// <returns></returns>
        [HttpGet("~/api/v1/pop3/letters")]
        [ProducesResponseType(typeof(OkResult), 200)]
        public IActionResult GetLetters()
        {
            return Ok();
        }

        /// <summary>
        /// Сохранение писем в базе данных связанно с учетной записью пользователя
        /// </summary>
        /// <returns></returns>
        [HttpPost("~/api/v1/pop3/letters")]
        [ProducesResponseType(typeof(OkResult), 200)]
        public IActionResult SaveLetters()
        {
            return Ok();
        }

        /// <summary>
        /// Удаление загруженных писем с почтового сервера
        /// </summary>
        /// <returns></returns>
        [HttpDelete("~/api/v1/pop3/letters")]
        [ProducesResponseType(typeof(OkResult), 200)]
        public IActionResult DeleteLetters()
        {
            return Ok();
        }
    }
}
