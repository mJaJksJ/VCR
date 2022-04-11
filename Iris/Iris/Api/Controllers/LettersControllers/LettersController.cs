using Iris.Database;
using Iris.Stores.ServiceConnectionStore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Iris.Api.Controllers.LettersControllers
{
    /// <summary>
    /// Контроллер получения писем
    /// </summary>
    public class LettersController : Controller
    {
        private readonly DatabaseContext _databaseContext;

        private readonly IServerConnectionStore _serverCollectionStore;

        /// <summary>
        /// .ctor
        /// </summary>
        public LettersController(DatabaseContext databaseContext, IServerConnectionStore serverConnectionStore)
        {
            _databaseContext = databaseContext;
            _serverCollectionStore = serverConnectionStore;
        }

        [HttpGet("~/api/letters/all/{userId}"), AllowAnonymous]
        [ProducesResponseType(typeof(int), 200)]
        public IActionResult GetAllLeters(int userId)
        {
            

            return Ok();
        }
    }
}
