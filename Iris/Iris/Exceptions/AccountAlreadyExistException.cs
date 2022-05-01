namespace Iris.Exceptions
{
    /// <summary>
    /// Учетная запись уже существует
    /// </summary>
    public class AccountAlreadyExistException : IrisException
    {
        /// <summary>
        /// .ctor
        /// </summary>
        public AccountAlreadyExistException(string name, string russianMessage = null) : base(russianMessage ?? $"Учетная запись {name} уже существует") { }

    }
}
