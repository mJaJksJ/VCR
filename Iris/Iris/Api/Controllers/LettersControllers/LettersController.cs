﻿using Iris.Helpers;
using Iris.Services.FormatLettersService;
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
        private readonly IFormatLettersSevice _formatLettersSevice;

        /// <summary>
        /// .ctor
        /// </summary>
        public LettersController(ILetterService letterService, IFormatLettersSevice formatLettersSevice)
        {
            _letterService = letterService;
            _formatLettersSevice = formatLettersSevice;
        }

        /// <summary>
        /// Получить письма по запросу
        /// </summary>
        /// <param name="lettersRequest"></param>
        /// <returns></returns>
        [HttpGet("~/api/letters")]
        [ProducesResponseType(typeof(IEnumerable<LetterContract>), 200)]
        public IActionResult GetLetters([FromQuery] LettersRequest lettersRequest)
        {
            var userId = User.GetUserId();
            var letters = _letterService.GetLetters(userId, lettersRequest);

            return Ok(letters);
        }

        /// <summary>
        /// Получить письма по запросу
        /// </summary>
        /// <param name="lettersRequest">Запрос писем</param>
        /// <param name="format">Формат</param>
        /// <returns></returns>
        [HttpGet("~/api/{format}/letters")]
        [ProducesResponseType(typeof(IEnumerable<LetterContract>), 200)]
        public IActionResult GetLetters(string format, [FromQuery] LettersRequest lettersRequest)
        {
            var userId = User.GetUserId();
            var needFormat = _formatLettersSevice.GetFormat(format);
            var letters = _letterService.GetLetters(userId, lettersRequest);
            var formattedLetters = _formatLettersSevice.FormatLetters(letters, needFormat);

            return Ok(formattedLetters);
        }
    }
}
