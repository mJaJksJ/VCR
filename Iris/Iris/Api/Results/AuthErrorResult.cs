namespace Iris.Api.Results
{
    /// <summary>
    /// Ошибка авторизации
    /// </summary>
    public class AuthErrorResult : ErrorResult
    {
        /// <summary>
        /// .ctor
        /// </summary>
        public AuthErrorResult(int code = 400, string message = "Неверный логин или пароль") : base(code, message)
        {
        }
    }
}
