using Iris.Common.Enums;
using Iris.Exceptions;
using MailKit;
using MailKit.Net.Imap;
using MailKit.Net.Pop3;

namespace Iris.Services.ConnectionProtocolHelperService
{
    /// <inheritdoc cref="IConnectionProtocolHelperService"/>
    public class ConnectionProtocolHelperService : IConnectionProtocolHelperService
    {
        /// <inheritdoc/>
        public ConnectionProtocol ByString(string protocol)
        {
            return protocol switch
            {
                "Pop3" => ConnectionProtocol.Pop3,
                "Imap" => ConnectionProtocol.Imap,
                _ => throw new UnknownProtocolException(protocol: protocol)
            };
        }

        /// <inheritdoc/>
        public IMailService GetConnection(ConnectionProtocol protocol)
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
