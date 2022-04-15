using Iris.Services.LettersService.Contracts;
using MailKit;
using MailKit.Net.Imap;

namespace Iris.Imap
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
                    Receivers = letter.To.Select(_ => new PersonContract(_)),
                    Subject = letter.Subject,
                    Date = letter.Date.UtcDateTime,
                    Text = letter.HtmlBody
                }); ;
            }

            return letters;
        }
    }
}
