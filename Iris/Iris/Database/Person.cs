namespace Iris.Database
{
    /// <summary>
    /// Персона почтового сообщения
    /// </summary>
    public class Person
    {
        /// <summary>
        /// Id персоны
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Имя персоны
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Почтовый адрес персоны
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Полученные письма
        /// </summary>
        public IEnumerable<Letter> ReceivedLetters { get; set; }

        /// <summary>
        /// Отправленные письма
        /// </summary>
        public IEnumerable<Letter> SentLetters { get; set; }
    }
}
