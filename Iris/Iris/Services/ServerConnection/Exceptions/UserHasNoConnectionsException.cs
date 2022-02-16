namespace Iris.Services.ServerConnection.Exceptions
{
    public class UserHasNoConnectionsException : Exception
    {
        private readonly string _message;

        public override string Message => _message;

        public UserHasNoConnectionsException(string message)
        {
            _message = message;
        }

        public UserHasNoConnectionsException() : this("Пользователь не имеет подключений к серверам") { }
    }
}
