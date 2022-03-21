using Iris.Services.MailServersService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Iris.Api.Controllers.ConnectionsControllers
{

    /// <summary>
    /// Контроллер работы с почтовыми серверами
    /// </summary>
    [Authorize]
    public class MailServersController : Controller
    {
        private readonly IMailServersService _mailServersService;

        public MailServersController(IMailServersService mailServersService)
        {
            _mailServersService = mailServersService;
        }

        [HttpGet("~/api/connections/mailservers/accounts/{userId}"), AllowAnonymous]
        [ProducesResponseType(typeof(IEnumerable<MailServerAccountContract>), 200)]
        public IActionResult GetMailServersAccounts(int userId)
        {
            var serverAccounts = _mailServersService.GetMailServerAccounts(userId);
            return Ok(serverAccounts);
        }

        [HttpPost("~/api/connections/mailservers"), AllowAnonymous]
        [ProducesResponseType(typeof(IEnumerable<MailServerAccountContract>), 200)]
        public IActionResult AddUserMailService(int userId)
        {
            var serverAccounts = _mailServersService.GetMailServerAccounts(userId);
            return Ok(serverAccounts);
        }
    }
}
