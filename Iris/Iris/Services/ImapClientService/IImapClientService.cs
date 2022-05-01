﻿using Iris.Api.Controllers.LettersControllers;
using Iris.Services.LettersService.Contracts;
using MailKit.Net.Imap;

namespace Iris.Services.ImapClientService
{
    /// <summary>
    /// Сервис для ImapClient
    /// </summary>
    public interface IImapClientService
    {
        /// <summary>
        /// Получить письма
        /// </summary>
        /// <param name="imapClient"></param>
        /// <param name="needAttachments">Получать ли вложения</param>
        /// <param name="accId">Id учетной записи</param>
        IEnumerable<LetterContract> GetLetters(ImapClient imapClient, NeedAttachments needAttachments, int accId);
    }
}
