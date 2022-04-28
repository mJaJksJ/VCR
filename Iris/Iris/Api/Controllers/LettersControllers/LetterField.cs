namespace Iris.Api.Controllers.LettersControllers
{
    /// <summary>
    /// Поля контракта письма
    /// </summary>
    public enum LetterField
    {
        /// <summary>
        /// Отправитель
        /// </summary>
        Sender,

        /// <summary>
        /// Получатели
        /// </summary>
        Receivers,

        /// <summary>
        /// Тема
        /// </summary>
        Subject,

        /// <summary>
        /// Дата
        /// </summary>
        Date,

        /// <summary>
        /// Текст
        /// </summary>
        Text,

        /// <summary>
        /// Вложения
        /// </summary>
        Attacments
    }
}
