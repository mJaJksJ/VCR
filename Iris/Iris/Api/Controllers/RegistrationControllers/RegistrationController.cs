using Iris.Api.Controllers.RegistrationControllers;
using Iris.Services.RegistrationService.cs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Iris.Api.Controllers.RegistrationController
{
    [Authorize]
    public class RegistrationController : Controller
    {
        private readonly IRegistrationService _registrationService;

        public RegistrationController(IRegistrationService registrationService)
        {
            _registrationService = registrationService;
        }

        [HttpPost("~/api/registration"), AllowAnonymous]
        [ProducesResponseType(typeof(RegistrationResponseContract), 200)]
        public IActionResult RegisterUser(RegistrationRequestContract contract)
        {
            return Ok(_registrationService.RegisterUser(contract));
        }
    }
}
