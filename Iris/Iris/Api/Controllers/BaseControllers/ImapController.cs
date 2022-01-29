using Microsoft.AspNetCore.Mvc;

namespace Iris.Api.Controllers.BaseControllers
{
    /// <summary>
    /// Функционал Imap
    /// </summary>
    public class ImapController : Controller
    {
        /// <summary>
        /// Загрузка писем на устройство пользователя (без вложений, с вложениями)
        /// </summary>
        /// <returns></returns>
        [HttpGet("~/api/v1/imap/letters")]
        [ProducesResponseType(typeof(OkResult), 200)]
        public IActionResult GetLetters()
        {
            return Ok();
        }

        /// <summary>
        /// Сохранение писем в базе данных связанно с учетной записью пользователя (без вложений, с вложениями)
        /// </summary>
        /// <returns></returns>
        [HttpPost("~/api/v1/imap/letters")]
        [ProducesResponseType(typeof(OkResult), 200)]
        public IActionResult SaveLetters()
        {
            return Ok();
        }

        /// <summary>
        /// Удаление сообщений
        /// </summary>
        /// <returns></returns>
        [HttpDelete("~/api/v1/imap/letters")]
        [ProducesResponseType(typeof(OkResult), 200)]
        public IActionResult DeleteLetters()
        {
            return Ok();
        }

        /// <summary>
        /// Получение информации о письмах без их загрузки
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("~/api/v1/imap/letter/{id}/info")]
        [ProducesResponseType(typeof(OkResult), 200)]
        public IActionResult GetLetterInfo(int id)
        {
            return Ok();
        }

        /// <summary>
        /// Eстановка флагов сообщений
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("~/api/v1/imap/letter/{id}/flag")]
        [ProducesResponseType(typeof(OkResult), 200)]
        public IActionResult SetFlag(int id)
        {
            return Ok();
        }
    }
}
