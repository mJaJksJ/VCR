using Iris.Api.Controllers.RegistrationControllers;
using Iris.Services.RegistrationService.cs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Iris.Api.Controllers.RegistrationController;

/// <summary>
///     Контроллер регистрации пользователей
/// </summary>
[Authorize]
public class RegistrationController : Controller
{
    private readonly IRegistrationService _registrationService;

    /// <summary>
    ///     .ctor
    /// </summary>
    public RegistrationController(IRegistrationService registrationService)
    {
        _registrationService = registrationService;
    }

    /// <summary>
    ///     Зарегистрировать пользователя
    /// </summary>
    /// <param name="contract">Контракт создания пользователя</param>
    [HttpPost("~/api/registration")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(RegistrationResponseContract), 200)]
    public IActionResult RegisterUser([FromBody] RegistrationRequestContract contract)
    {
        return Ok(_registrationService.RegisterUser(contract));
    }
}