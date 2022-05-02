using Iris.Api.Controllers.LettersControllers;
using Iris.Database;
using Iris.Exceptions;
using Iris.Services.ImapClientService;
using Iris.Services.LettersService.Contracts;
using Iris.Stores.ServiceConnectionStore;
using MailKit.Net.Imap;
using MailKit.Net.Pop3;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Iris.Services.LettersService
{
    /// <inheritdoc />
    public class LetterService : ILetterService
    {
        private readonly IServerConnectionStore _serverConnectionStore;
        private readonly DatabaseContext _context;
        private readonly IImapClientService _imapClientService;

        /// <summary>
        /// .ctor
        /// </summary>
        public LetterService(IServerConnectionStore serverConnectionStore, DatabaseContext context, IImapClientService imapClientService)
        {
            _serverConnectionStore = serverConnectionStore;
            _context = context;
            _imapClientService = imapClientService;
        }

        /// <inheritdoc/>
        public void ChangeFlag(int userId, int accId, int letterId, int flag)
        {
            var connection = _serverConnectionStore.GetUserConnection(userId, accId);
            if (connection.MailService is ImapClient imapClient)
            {
                _imapClientService.ChangeFlag(imapClient, letterId, flag);
            }
            else
            {
                throw new NotImapException();
            }
        }

        /// <inheritdoc/>
        public void RemoveLetter(int userId, int accId, int letterId)
        {
            var connection = _serverConnectionStore.GetUserConnection(userId, accId);
            if (connection.MailService is ImapClient imapClient)
            {
                _imapClientService.RemoveLetter(imapClient, letterId);
            }
            else
            {
                throw new NotImapException();
            }
        }

        /// <inheritdoc/>
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

            if (lettersRequest.Sort != null)
            {
                letters = Sort(lettersRequest.Sort, letters).ToList();
            }

            if (lettersRequest.SaveLettersToLocalBd)
            {
                foreach (var _ in letters)
                {
                    var sender = _context.Persons.Add(new Person
                    {
                        Name = _.Sender.Name,
                        Email = _.Sender.Email
                    });
                    _context.SaveChanges();

                    var receivers = new List<Person>();
                    foreach (var r in _.Receivers)
                    {
                        var receiver = _context.Persons.Add(new Person
                        {
                            Name = r.Name,
                            Email = r.Email,
                        });
                        receivers.Add(receiver.Entity);
                    }

                    _context.Letters.Add(new Letter()
                    {
                        SenderId = sender.Entity.Id,
                        AccountId = _.AccountId,
                        Attachments = _.Attachments.Select(a => new Attachment
                        {
                            Blob = a.Blob,
                            Name = a.Name
                        }).ToList(),
                        Date = _.Date,
                        Receivers = receivers,
                        Subject = _.Subject,
                        Text = _.Text
                    });
                }

                _context.SaveChanges();
            }

            return letters;
        }

        private IEnumerable<LetterContract> GetLettersFromDb(int accId, NeedAttachments needAttachments)
        {
            var letters = _context.Accounts
                .FirstOrDefault(_ => _.Id == accId)
                ?.Letters;

            var letterContracts = new List<LetterContract>();

            foreach (var _ in letters)
            {
                var letter = new LetterContract
                {
                    Id = _.Id.ToString(),
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
                    Attachments = new List<AttachmentContract>(),
                    AccountId = accId
                };

                switch (needAttachments)
                {
                    case NeedAttachments.OnlyName:
                        if (_.Attachments.Any())
                        {
                            var attachs = _.Attachments.ToArray();
                            letter.Attachments.AddRange(attachs.Select(a => new AttachmentContract
                            {
                                Name = a.Name
                            }));
                        }
                        break;

                    case NeedAttachments.WithoutAttachments:
                        // nothing
                        break;

                    case NeedAttachments.WithAttachmentsBlob:
                        if (_.Attachments.Any())
                        {
                            var attachs = _.Attachments.ToArray();
                            letter.Attachments.AddRange(attachs.Select(a => new AttachmentContract
                            {
                                Name = a.Name,
                                Blob = a.Blob
                            }));
                        }
                        break;

                    default:
                        throw new Exception();
                }

                letterContracts.Add(letter);
            }

            return letterContracts;
        }

        private IEnumerable<LetterContract> GetLettersFromServer(ServerConnection connection, NeedAttachments needAttachments, int accId)
        {
            return connection.MailService switch
            {
                ImapClient imapClient => _imapClientService.GetLetters(imapClient, needAttachments, accId),
                Pop3Client pop3Client => throw new Exception(),
                _ => throw new Exception(),
            };
        }

        private static IEnumerable<LetterContract> Filter(FilterLetter filter, IEnumerable<LetterContract> letters)
        {
            return filter.Field switch
            {
                LetterField.Attacments => letters.Where(_ => filter.Templates.All(t => _.Attachments.Any(a => filter.IsRegex
                        ? Regex.IsMatch(a.Name, t)
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
                        ? Regex.IsMatch(a.ToString(), t)
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
