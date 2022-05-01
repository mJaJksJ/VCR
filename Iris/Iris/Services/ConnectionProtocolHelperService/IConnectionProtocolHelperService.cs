using Iris.Common.Enums;
using Iris.Exceptions;
using MailKit;

namespace Iris.Services.ConnectionProtocolHelperService
{
    /// <summary>
    /// Сервис работы с ConnectionProtocol
    /// </summary>
    public interface IConnectionProtocolHelperService
    {
        /// <summary>
        /// Получить протокол по его названию
        /// </summary>
        /// <param name="protocol">Название протокола</param>
        /// <exception cref="UnknownProtocolException"></exception>
        ConnectionProtocol ByString(string protocol);


        /// <summary>
        /// Получить интерфейс подключения к почтовому сервису
        /// </summary>
        /// <param name="protocol"></param>
        /// <exception cref="UnknownProtocolException"></exception>
        IMailService GetConnection(ConnectionProtocol protocol);
    }
}
