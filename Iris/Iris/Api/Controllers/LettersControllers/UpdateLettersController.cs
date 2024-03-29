﻿using Iris.Services.ClaimsPrincipalHelperService;
using Iris.Services.LettersService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Iris.Api.Controllers.LettersControllers;

/// <summary>
///     Контроллер изменения писем
/// </summary>
[Authorize]
public class UpdateLettersController : Controller
{
    private readonly IClaimsPrincipalHelperService _claimsPrincipalHelperService;
    private readonly ILetterService _letterService;

    /// <summary>
    ///     .ctor
    /// </summary>
    public UpdateLettersController(ILetterService letterService,
        IClaimsPrincipalHelperService claimsPrincipalHelperService)
    {
        _letterService = letterService;
        _claimsPrincipalHelperService = claimsPrincipalHelperService;
    }

    /// <summary>
    ///     Установить флаг
    /// </summary>
    /// <param name="accId">Id учетной записи</param>
    /// <param name="letterId">Id письма</param>
    /// <param name="flag">Флаг</param>
    [HttpPost("~/api/letters/accaunt/{accId}/letter/{letterId}/flag/{flag}")]
    [ProducesResponseType(typeof(OkResult), 200)]
    public IActionResult ChangeFlag(int accId, string letterId, int flag)
    {
        var userId = _claimsPrincipalHelperService.GetUserId(User);
        _letterService.ChangeFlag(userId, accId, letterId, flag);

        return Ok();
    }

    /// <summary>
    ///     Удалить письио
    /// </summary>
    /// <param name="accId">Id учетной записи</param>
    /// <param name="letterId">Id письма</param>
    [HttpDelete("~/api/letters/accaunt/{accId}/letter/{letterId}")]
    [ProducesResponseType(typeof(OkResult), 200)]
    public IActionResult RemoveLetter(int accId, string letterId)
    {
        var userId = _claimsPrincipalHelperService.GetUserId(User);
        _letterService.RemoveLetter(userId, accId, letterId);

        return Ok();
    }
}