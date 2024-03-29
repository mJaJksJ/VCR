﻿using Iris.Database;
using Iris.Services.ClaimsPrincipalHelperService;
using Iris.Services.MailServersService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Iris.Api.Controllers.ConnectionsControllers;

/// <summary>
///     Контроллер работы с почтовыми серверами
/// </summary>
[Authorize]
public class MailServersController : Controller
{
    private readonly IClaimsPrincipalHelperService _claimsPrincipalHelperService;
    private readonly IMailServersService _mailServersService;

    /// <summary>
    ///     .ctor
    /// </summary>
    public MailServersController(IMailServersService mailServersService,
        IClaimsPrincipalHelperService claimsPrincipalHelperService)
    {
        _mailServersService = mailServersService;
        _claimsPrincipalHelperService = claimsPrincipalHelperService;
    }

    /// <summary>
    ///     Получить список аккаунтов к почтовым серверам пользователя
    /// </summary>
    [HttpGet("~/api/connections/mailservers/accounts/users")]
    [ProducesResponseType(typeof(IEnumerable<MailServerAccountContract>), 200)]
    public IActionResult GetMailServersAccounts()
    {
        var userId = _claimsPrincipalHelperService.GetUserId(User);
        var serverAccounts = _mailServersService.GetMailServerAccounts(userId);
        return Ok(serverAccounts);
    }

    /// <summary>
    ///     Получить список доступных почтовых серверов
    /// </summary>
    [HttpGet("~/api/connections/mailservers/accounts/availables")]
    [ProducesResponseType(typeof(IEnumerable<MailServer>), 200)]
    public IActionResult GetAvailableMailServers()
    {
        var userId = _claimsPrincipalHelperService.GetUserId(User);
        var serverAccounts = _mailServersService.GetAvailableMailServers(userId);
        return Ok(serverAccounts);
    }

    /// <summary>
    ///     Добавить новый почтовый сервер
    /// </summary>
    /// <param name="mailServerContract">Контракт добавления сервера</param>
    [HttpPost("~/api/connections/mailservers")]
    [ProducesResponseType(typeof(void), 200)]
    public IActionResult AddUserMailService([FromBody] MailServerContract mailServerContract)
    {
        _mailServersService.NewMailServer(mailServerContract);
        return Ok();
    }
}