using Iris.Common.Enums;
using Iris.Exceptions;
using MailKit;
using MailKit.Net.Imap;
using MailKit.Net.Pop3;

namespace Iris.Helpers
{
    /// <summary>
    /// Расширения для ConnectionProtocol
    /// </summary>
    public static class ConnectionProtocolHelper
    {
        /// <summary>
        /// Получить протокол по его названию
        /// </summary>
        /// <param name="protocol">Название протокола</param>
        /// <exception cref="UnknownProtocolException"></exception>
        public static ConnectionProtocol ByString(string protocol)
        {
            return protocol switch
            {
                "Pop3" => ConnectionProtocol.Pop3,
                "Imap" => ConnectionProtocol.Imap,
                _ => throw new UnknownProtocolException(protocol: protocol)
            };
        }

        /// <summary>
        /// Получить интерфейс подключения к почтовому сервису
        /// </summary>
        /// <param name="protocol"></param>
        /// <exception cref="UnknownProtocolException"></exception>
        public static IMailService GetConnection(this ConnectionProtocol protocol)
        {
            return protocol switch
            {
                ConnectionProtocol.Pop3 => new Pop3Client(),
                ConnectionProtocol.Imap => new ImapClient(),
                _ => throw new UnknownProtocolException(protocol: protocol.ToString())
            };
        }
    }
}
