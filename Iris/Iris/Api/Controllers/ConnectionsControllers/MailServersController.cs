﻿using Iris.Services.MailServersService;
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

        /// <summary>
        /// .ctor
        /// </summary>
        public MailServersController(IMailServersService mailServersService)
        {
            _mailServersService = mailServersService;
        }

        /// <summary>
        /// Получить список аккаунтов к почтовым серверам пользователя
        /// </summary>
        /// <param name="userId">Id пользователя</param>
        [HttpGet("~/api/connections/mailservers/accounts/{userId}"), AllowAnonymous]
        [ProducesResponseType(typeof(IEnumerable<MailServerAccountContract>), 200)]
        public IActionResult GetMailServersAccounts(int userId)
        {
            var serverAccounts = _mailServersService.GetMailServerAccounts(userId);
            return Ok(serverAccounts);
        }

        /// <summary>
        /// Добавить новый почтовый сервер
        /// </summary>
        /// <param name="mailServerContract">Контракт добавления сервера</param>
        [HttpPost("~/api/connections/mailservers"), AllowAnonymous]
        [ProducesResponseType(typeof(void), 200)]
        public IActionResult AddUserMailService([FromBody] MailServerContract mailServerContract)
        {
            _mailServersService.NewMailServer(mailServerContract);
            return Ok();
        }
    }
}
