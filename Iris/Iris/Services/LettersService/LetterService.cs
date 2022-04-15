using Iris.Imap;
using Iris.Services.LettersService.Contracts;
using Iris.Stores.ServiceConnectionStore;
using MailKit.Net.Imap;
using MailKit.Net.Pop3;

namespace Iris.Services.LettersService
{
    /// <inheritdoc />
    public class LetterService : ILetterService
    {
        private readonly IServerConnectionStore _serverConnectionStore;

        /// <summary>
        /// .ctor
        /// </summary>
        public LetterService(IServerConnectionStore serverConnectionStore)
        {
            _serverConnectionStore = serverConnectionStore;
        }

        /// <inheritdoc/>
        public IEnumerable<LetterContract> GetAllLetters(int userId)
        {
            var connections = _serverConnectionStore.GetUserConnections(userId);

            var letters = new List<LetterContract>();

            foreach (var connection in connections)
            {
                switch (connection.MailService)
                {
                    case ImapClient imapClient:
                        letters.AddRange(imapClient.GetAllLetters());
                        break;

                    case Pop3Client pop3Client:
                        break;

                    default:
                        throw new Exception();
                }
            }

            return letters.OrderBy(_ => _.Date);
        }
    }
}
