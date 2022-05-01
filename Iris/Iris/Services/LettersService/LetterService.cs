using System.Globalization;
using System.Text.RegularExpressions;
using Iris.Api.Controllers.LettersControllers;
using Iris.Database;
using Iris.Exceptions;
using Iris.Helpers.Imap;
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
                        letters.AddRange(GetLettersFromServer(connection, needAttachments, accId));
                        break;

                    case LettersFrom.LocalDb:
                        letters.AddRange(GetLettersFromDb(accId, needAttachments));
                        break;

                    case LettersFrom.LocalAndRemote:
                        letters.AddRange(GetLettersFromServer(connection, needAttachments, accId));
                        letters.AddRange(GetLettersFromDb(accId, needAttachments));
                        break;

                    default:
                        throw new Exception();
                }
            }

            foreach (var filter in lettersRequest.Filters)
            {
                letters = Filter(filter, letters).ToList();
            }

            letters = Sort(lettersRequest.Sort, letters).ToList();

            if (lettersRequest.SaveLettersToLocalBd)
            {
                _context.Letters.AddRange(letters.Select(_ => new Letter()
                {
                    Sender = new Person
                    {
                        Name = _.Sender.Name,
                        Email = _.Sender.Email,
                    },
                    AccoundId = _.AccoundId,
                    Attacments = _.Attacments.Select(a => new Attachment
                    {
                        Blob = a.Blob,
                        Name = a.Name
                    }).ToList(),
                    Date = _.Date,
                    Receivers = _.Receivers.Select(r => new Person
                    {
                        Name = r.Name,
                        Email = r.Email,
                    }).ToList(),
                    Subject = _.Subject,
                    Text = _.Text
                }));
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
                    }).ToList(),
                    AccoundId = accId
                });

            return letters;
        }

        private IEnumerable<LetterContract> GetLettersFromServer(ServerConnection connection, NeedAttachments needAttachments, int accId)
        {
            return connection.MailService switch
            {
                ImapClient imapClient => imapClient.GetLetters(needAttachments, accId),
                Pop3Client pop3Client => throw new Exception(),
                _ => throw new Exception(),
            };
        }

        private static IEnumerable<LetterContract> Filter(FilterLetter filter, IEnumerable<LetterContract> letters)
        {
            return filter.Field switch
            {
                LetterField.Attacments => letters.Where(_ => filter.Templates.All(t => _.Attacments.Any(a => filter.IsRegex
                        ? Regex.IsMatch(a.Name, filter.Template)
                        : a.Name.Contains(filter.Template))
                    )
                ),
                LetterField.Text => letters.Where(_ => filter.IsRegex
                    ? Regex.IsMatch(_.Text, filter.Template)
                    : _.Text.Contains(filter.Template)
                ),
                LetterField.Date => letters.Where(_ => filter.IsRegex
                    ? Regex.IsMatch(_.Date.ToString(CultureInfo.InvariantCulture), filter.Template)
                    : _.Date.ToString(CultureInfo.InvariantCulture).Contains(filter.Template)
                ),
                LetterField.Receivers => letters.Where(_ => filter.Templates.All(t => _.Receivers.Any(a => filter.IsRegex
                        ? Regex.IsMatch(a.ToString(), filter.Template)
                        : a.ToString().Contains(filter.Template))
                    )
                ),
                LetterField.Sender => letters.Where(_ => filter.IsRegex
                    ? Regex.IsMatch(_.Sender.ToString(), filter.Template)
                    : _.Text.Contains(filter.Template)
                ),
                LetterField.Subject => letters.Where(_ => filter.IsRegex
                    ? Regex.IsMatch(_.Subject, filter.Template)
                    : _.Subject.Contains(filter.Template)
                ),
                _ => throw new UnknownFieldException(filter.Field.ToString()),
            };
        }

        private static IEnumerable<LetterContract> Sort(SortLetter filter, IEnumerable<LetterContract> letters)
        {
            return filter.Field switch
            {
                LetterField.Date => (filter.SortBy == SortBy.Asc
                    ? letters.OrderBy(_ => _.Date)
                    : letters.OrderByDescending(_ => _.Date))
                    .ThenBy(_ => _.Id),

                LetterField.Sender => (filter.SortBy == SortBy.Asc
                    ? letters.OrderBy(_ => _.Sender)
                    : letters.OrderByDescending(_ => _.Sender))
                    .ThenBy(_ => _.Id),
                LetterField.Subject => (filter.SortBy == SortBy.Asc
                    ? letters.OrderBy(_ => _.Subject)
                    : letters.OrderByDescending(_ => _.Subject))
                    .ThenBy(_ => _.Id),
                _ => throw new UnsupportedSortFieldException(filter.Field.ToString()),
            };
        }
    }
}
