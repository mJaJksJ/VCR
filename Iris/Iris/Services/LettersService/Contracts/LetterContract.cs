namespace Iris.Services.LettersService.Contracts
{
    /// <summary>
    /// Котракт письма
    /// </summary>
    public class LetterContract
    {
        /// <summary>
        /// Id письма
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Отправитель
        /// </summary>
        public PersonContract Sender { get; set; }

        /// <summary>
        /// Получатели
        /// </summary>
        public IEnumerable<PersonContract> Receivers { get; set; }

        /// <summary>
        /// Тема
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// Дата получения/отправки
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Текст
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Вложения
        /// </summary>
        public IEnumerable<AttachmentContract> Attacments { get; set; }
    }
}
