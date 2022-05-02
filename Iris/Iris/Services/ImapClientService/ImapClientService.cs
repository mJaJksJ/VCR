using Iris.Api.Controllers.LettersControllers;
using Iris.Exceptions;
using Iris.Services.LettersService.Contracts;
using MailKit;
using MailKit.Net.Imap;
using MimeKit;

namespace Iris.Services.ImapClientService
{
    /// <inheritdoc cref="Pop3ClientService"/>
    public class ImapClientService : IImapClientService
    {
        private static readonly Serilog.ILogger Log = Serilog.Log.ForContext<ImapClientService>();

        /// <inheritdoc/> 
        public void ChangeFlag(ImapClient imapClient, int letterId, int flag)
        {
            var inboxFolder = imapClient.Inbox;
            inboxFolder.Store(new UniqueId((uint)letterId), new StoreFlagsRequest(StoreAction.Add, (MessageFlags)flag) { Silent = true });
        }

        /// <inheritdoc/>
        public void RemoveLetter(ImapClient imapClient, int letterId)
        {
            var inboxFolder = imapClient.Inbox;
            inboxFolder.Store(new UniqueId((uint)letterId), new StoreFlagsRequest(StoreAction.Add, MessageFlags.Deleted) { Silent = true });
            inboxFolder.Expunge();
        }

        /// <inheritdoc/>
        public IEnumerable<LetterContract> GetLetters(ImapClient imapClient, NeedAttachments needAttachments, int accId)
        {
            var inboxFolder = imapClient.Inbox;
            inboxFolder.Open(FolderAccess.ReadOnly);

            var letters = new List<LetterContract>();

            foreach (var letter in inboxFolder)
            {
                LetterContract letterContract;
                try
                {
                    letterContract = new LetterContract
                    {
                        Sender = new PersonContract(letter.From.Mailboxes.FirstOrDefault()),
                        Receivers = letter.To.Select(_ => new PersonContract(_)).ToList(),
                        Subject = letter.Subject,
                        Date = letter.Date.UtcDateTime,
                        Text = letter.HtmlBody,
                        Attachments = new List<AttachmentContract>(),
                        AccountId = accId,
                        Id = letter.MessageId
                    };
                }
                catch (ParsePersonInternetAddressException e)
                {
                    Log.Error(e, e.Message);
                    letterContract = new LetterContract
                    {
                        Attachments = new List<AttachmentContract>()
                    };
                }

                switch (needAttachments)
                {
                    case NeedAttachments.OnlyName:
                        if (letter.Attachments.Any())
                        {
                            var attachs = letter.Attachments.ToArray();
                            letterContract.Attachments.AddRange(attachs.Select(_ => new AttachmentContract
                            {
                                Name = (_ as MimePart).FileName
                            }));
                        }
                        break;

                    case NeedAttachments.WithoutAttachments:
                        // nothing
                        break;

                    case NeedAttachments.WithAttachmentsBlob:
                        if (letter.Attachments.Any())
                        {
                            var attachs = letter.Attachments.ToArray();
                            letterContract.Attachments.AddRange(attachs.Select(_ => new AttachmentContract
                            {
                                Name = (_ as MimePart).FileName,
                                Blob = new BinaryReader((_ as MimePart).Content.Stream).ReadBytes((int)(_ as MimePart).Content.Stream.Length)
                            }));
                        }
                        break;

                    default:
                        throw new Exception();
                }

                letters.Add(letterContract);
            }

            return letters;
        }
    }
}
