namespace Iris.Database
{
    /// <summary>
    /// Письмо
    /// </summary>
    public class Letter
    {
        /// <summary>
        /// Id письма
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Отправитель
        /// </summary>
        public Person Sender { get; set; }

        /// <summary>
        /// Id отправителя
        /// </summary>
        public int SenderId { get; set; }

        /// <summary>
        /// Получатели
        /// </summary>
        public IEnumerable<Person> Receivers { get; set; }

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

        public IEnumerable<Attachment> Attachments { get; set; }

        /// <summary>
        /// Id учетной записи
        /// </summary>
        public int AccountId { get; set; }

        /// <summary>
        /// Учетная запись
        /// </summary>
        public Account Account { get; set; }

    }
}
