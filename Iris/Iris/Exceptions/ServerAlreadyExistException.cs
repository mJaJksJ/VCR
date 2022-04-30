namespace Iris.Exceptions
{
    public class ServerAlreadyExistException : IrisException
    {
        /// <summary>
        /// .ctor
        /// </summary>
        public ServerAlreadyExistException(string host, int port, string russianMessage = null) : base(russianMessage ?? $"Сервер {host}:{port} уже существует") { }

    }
}
