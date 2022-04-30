using Iris.Api.Controllers.LettersControllers;
using Iris.Database;
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
        private readonly DatabaseContext _context;

        /// <summary>
        /// .ctor
        /// </summary>
        public LetterService(IServerConnectionStore serverConnectionStore, DatabaseContext context)
        {
            _serverConnectionStore = serverConnectionStore;
            _context = context;
        }

        /// <summary>
        /// Получить письма
        /// </summary>
        /// <param name="userId">Id пользователя</param>
        /// <param name="lettersRequest">Параметры для фильтрации, сортировки и пагинации при получении писем</param>
        /// <exception cref="Exception"></exception>
        public IEnumerable<LetterContract> GetLetters(int userId, LettersRequest lettersRequest)
        {
            var accIds = lettersRequest.AccountsSettings.Select(_ => _.Key).AsEnumerable();

            var connections = accIds.Any()
                ? _serverConnectionStore.GetUserConnections(userId, accIds)
                : _serverConnectionStore.GetUserConnections(userId);

            var letters = new List<LetterContract>();

            foreach (var connection in connections)
            {
                var accId = connection.Account.Id;
                LettersFrom letterFrom;
                NeedAttachments needAttachments;

                if (lettersRequest.AccountsSettings.ContainsKey(accId))
                {
                    letterFrom = lettersRequest.AccountsSettings[accId].LettersStorage;
                    needAttachments = lettersRequest.AccountsSettings[accId].NeedAttachments;
                }
                else
                {
                    letterFrom = lettersRequest.GlobalSettings.LettersStorage;
                    needAttachments = lettersRequest.GlobalSettings.NeedAttachments;
                }

                switch (letterFrom)
                {
                    case LettersFrom.Server:
                        letters.AddRange(GetLettersFromServer(connection, needAttachments));
                        break;

                    case LettersFrom.LocalDb:
                        letters.AddRange(GetLettersFromDb(accId, needAttachments));
                        break;

                    case LettersFrom.LocalAndRemote:
                        letters.AddRange(GetLettersFromServer(connection, needAttachments));
                        letters.AddRange(GetLettersFromDb(accId, needAttachments));
                        break;

                    default:
                        throw new Exception();
                }
            }

            foreach (var filter in lettersRequest.Filters)
            {
                //TODO
            }

            foreach (var sort in lettersRequest.Sorts)
            {
                //TODO
            }

            if (lettersRequest.SaveLettersToLocalBd)
            {
                //TODO
            }

            return letters;
        }

        private IEnumerable<LetterContract> GetLettersFromDb(int accId, NeedAttachments needAttachments)
        {
            var letters = _context.Accounts
                .FirstOrDefault(_ => _.Id == accId)
                ?.Letters
                ?.Select(_ => new LetterContract
                {
                    Id = _.Id,
                    Sender = new PersonContract
                    {
                        Id = _.Sender.Id,
                        Name = _.Sender.Name,
                        Email = _.Sender.Email,
                    },
                    Receivers = _.Receivers.Select(r => new PersonContract
                    {
                        Id = r.Id,
                        Name = r.Name,
                        Email = r.Email,
                    }).ToList(),
                    Subject = _.Subject,
                    Date = _.Date,
                    Text = _.Text,
                    Attacments = _.Attacments.Select(a => new AttachmentContract
                    {
                        Id = a.Id,
                        Blob = a.Blob,
                        Name = a.Name
                    }).ToList()
                });

            return letters;
        }

        private IEnumerable<LetterContract> GetLettersFromServer(ServerConnection connection, NeedAttachments needAttachments)
        {
            return connection.MailService switch
            {
                ImapClient imapClient => imapClient.GetLetters(needAttachments),
                Pop3Client pop3Client => throw new Exception(),
                _ => throw new Exception(),
            };
        }
    }
}
