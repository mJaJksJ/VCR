﻿using Iris.Api.Controllers.LettersControllers;
using Iris.Services.LettersService.Contracts;
using MailKit;
using MailKit.Net.Imap;
using MimeKit;

namespace Iris.Helpers.Imap
{
    /// <summary>
    /// Хелпер для ImapClient
    /// </summary>
    public static class ImapClientHelper
    {
        /// <summary>
        /// Получить все письма
        /// </summary>
        /// <param name="imapClient"></param>
        /// <returns></returns>
        public static IEnumerable<LetterContract> GetAllLetters(this ImapClient imapClient)
        {
            var inboxFolder = imapClient.Inbox;
            inboxFolder.Open(FolderAccess.ReadOnly);

            var letters = new List<LetterContract>();

            foreach (var letter in inboxFolder)
            {
                letters.Add(new LetterContract
                {
                    Sender = new PersonContract(letter.From[0]),
                    Receivers = letter.To.Select(_ => new PersonContract(_)).ToList(),
                    Subject = letter.Subject,
                    Date = letter.Date.UtcDateTime,
                    Text = letter.HtmlBody
                }); ;
            }

            return letters;
        }

        /// <summary>
        /// Получить письма
        /// </summary>
        /// <param name="imapClient"></param>
        /// <param name="needAttachments">Получать ли вложения</param>
        /// <param name="accId">Id учетной записи</param>
        public static IEnumerable<LetterContract> GetLetters(this ImapClient imapClient, NeedAttachments needAttachments, int accId)
        {
            var inboxFolder = imapClient.Inbox;
            inboxFolder.Open(FolderAccess.ReadOnly);

            var letters = new List<LetterContract>();

            foreach (var letter in inboxFolder)
            {
                var letterContract = new LetterContract
                {
                    Sender = new PersonContract(letter.From[0]),
                    Receivers = letter.To.Select(_ => new PersonContract(_)).ToList(),
                    Subject = letter.Subject,
                    Date = letter.Date.UtcDateTime,
                    Text = letter.HtmlBody,
                    Attacments = new List<AttachmentContract>(),
                    AccoundId = accId
                };

                switch (needAttachments)
                {
                    case NeedAttachments.OnlyName:
                        if (letter.Attachments.Any())
                        {
                            var attachs = letter.Attachments.ToArray();
                            letterContract.Attacments.AddRange(attachs.Select(_ => new AttachmentContract
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
                            letterContract.Attacments.AddRange(attachs.Select(_ => new AttachmentContract
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
