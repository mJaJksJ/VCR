using Microsoft.AspNetCore.Mvc;

namespace Iris.Api.Controllers.BaseControllers
{
    /// <summary>
    /// Пользовательские почтовые сервера
    /// </summary>
    public class ServersController: Controller
    {
        /// <summary>
        /// Добавить пользовательский сервис
        /// </summary>
        /// <returns></returns>
        [HttpGet("~/api/v1/server")]
        [ProducesResponseType(typeof(OkResult), 200)]
        public IActionResult AddServer()
        {
            return Ok();
        }

        /// <summary>
        /// Удалить пользовательский сервис
        /// </summary>
        /// <returns></returns>
        [HttpDelete("~/api/v1/server")]
        [ProducesResponseType(typeof(OkResult), 200)]
        public IActionResult DeleteServer()
        {
            return Ok();
        }
    }
}
