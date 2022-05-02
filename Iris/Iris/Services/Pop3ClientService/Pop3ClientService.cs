using Iris.Api.Controllers.LettersControllers;
using Iris.Exceptions;
using Iris.Services.LettersService.Contracts;
using MailKit;
using MailKit.Net.Imap;
using MailKit.Net.Pop3;
using MimeKit;

namespace Iris.Services.Pop3ClientService
{
    /// <inheritdoc cref="Pop3ClientService"/>
    public class Pop3ClientService : IPop3ClientService
    {
        private static readonly Serilog.ILogger Log = Serilog.Log.ForContext<Pop3ClientService>();

        /// <inheritdoc/>
        public IEnumerable<LetterContract> GetLetters(Pop3Client pop3Client, NeedAttachments needAttachments, int accId)
        {
            var letters = new List<LetterContract>();

            for (int i = 0; i < pop3Client.Count; i++)
            {
                var letter = pop3Client.GetMessage(i);
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
                        AccountId = accId
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
