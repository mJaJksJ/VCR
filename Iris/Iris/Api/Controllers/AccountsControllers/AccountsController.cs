using Iris.Helpers;
using Iris.Services.AccountsService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Iris.Api.Controllers.AccountsControllers
{
    /// <summary>
    /// Контроллер работы с учетными записями
    /// </summary>
    [Authorize]
    public class AccountsController : Controller
    {
        private readonly IAccountsService _accountsService;

        /// <summary>
        /// .ctor
        /// </summary>
        /// <param name="accountsService"></param>
        public AccountsController(IAccountsService accountsService)
        {
            _accountsService = accountsService;
        }

        /// <summary>
        /// Добавить новую учетную запись
        /// </summary>
        /// <param name="contract">Запрос добавления новой учетной записи</param>
        [HttpPost("~/api/accounts/add"), AllowAnonymous]
        [ProducesResponseType(typeof(void), 200)]
        public IActionResult AddNewAccount([FromBody] AccountRequestContract contract)
        {
            var userId = User.GetUserId();
            _accountsService.AddNewAccount(userId, contract);
            return Ok();
        }

        /// <summary>
        /// Удалить учетную запись
        /// </summary>
        /// <param name="accId">Id учетной записи</param>
        [HttpDelete("~/api/accounts/{accId}"), AllowAnonymous]
        [ProducesResponseType(typeof(void), 200)]
        public IActionResult RemoveAccount(int accId)
        {
            var userId = User.GetUserId();
            _accountsService.RemoveAccount(userId, accId);
            return Ok();
        }
    }
}
