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


        #region GetAllLetters
        /// <summary>
        /// Получить все письма
        /// </summary>
        [HttpGet("~/api/letters/all")]
        [ProducesResponseType(typeof(IEnumerable<LetterContract>), 200)]
        public IActionResult GetAllLetters()
        {
            var userId = User.GetUserId();
            var letters = _letterService.GetAllLetters(userId);

            return Ok(letters);
        }

        /// <summary>
        /// Получить все письма
        /// </summary>
        [HttpGet("~/api/{format}/letters/all")]
        [ProducesResponseType(typeof(IEnumerable<LetterContract>), 200)]
        public IActionResult GetAllLetters(string format)
        {
            var userId = User.GetUserId();
            var needFormat = _formatLettersSevice.GetFormat(format); 
            var letters = _letterService.GetAllLetters(userId);
            var formattedLetters = _formatLettersSevice.FormatLetters(letters, needFormat);

            return Ok(formattedLetters);
        }

        #endregion
    }
}
