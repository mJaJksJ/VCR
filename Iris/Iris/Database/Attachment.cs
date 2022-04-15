namespace Iris.Database
{
    /// <summary>
    /// Вложение
    /// </summary>
    public class Attachment
    {
        /// <summary>
        /// Id вложения
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Название
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Блоб
        /// </summary>
        public byte[] Blob { get; set; }

        /// <summary>
        /// Письмо
        /// </summary>
        public Letter Letter { get; set; }

        /// <summary>
        /// Id письма
        /// </summary>
        public int LetterId { get; set; }
    }
}
