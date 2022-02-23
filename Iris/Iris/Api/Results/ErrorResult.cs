using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Iris.Api.Results
{
    /// <summary>
    /// Ответ об ошибке
    /// </summary>
    public class ErrorResult : IActionResult
    {
        /// <summary>
        /// .ctor
        /// </summary>
        public ErrorResult(int code, string message)
        {
            Code = code;
            ErrorMessage = message;
        }

        /// <summary>
        /// Http-код ошибки
        /// </summary>
        [JsonProperty("code")]
        public int Code { get; }

        /// <summary>
        /// Сообщение об ошибке
        /// </summary>
        [JsonProperty("error_message")]
        public string ErrorMessage { get; }

        /// <inheritdoc/>
        public Task ExecuteResultAsync(ActionContext context)
        {
            var result = new ObjectResult(this) { StatusCode = Code };
            return result.ExecuteResultAsync(context);
        }
    }
}
