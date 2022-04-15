using Iris.Helpers;
using Iris.Services.LettersService;
using Iris.Services.LettersService.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Iris.Api.Controllers.LettersControllers
{
    /// <summary>
    /// Контроллер получения писем
    /// </summary>
    [Authorize]
    public class LettersController : Controller
    {
        private readonly ILetterService _letterService;

        /// <summary>
        /// .ctor
        /// </summary>
        public LettersController(ILetterService letterService)
        {
            _letterService = letterService;
        }

        /// <summary>
        /// Получить все письма
        /// </summary>
        [HttpGet("~/api/letters/all")]
        [ProducesResponseType(typeof(IEnumerable<LetterContract>), 200)]
        public IActionResult GetAllLeters()
        {
            var userId = User.GetUserId();
            var letters = _letterService.GetAllLetters(userId);

            return Ok(letters);
        }
    }
}
