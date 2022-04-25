namespace Iris.Api.Controllers.LettersControllers
{
    /// <summary>
    /// Параметры для фильтрации и сортировки при получении писем
    /// </summary>
    public class LettersRequest
    {
        public Dictionary<int, Settings> AccountsSettings { get; set; }

        public Settings GlobalSettings { get; set; }

        public bool SaveLettersToLocalBd { get; set; }

        public IEnumerable<FilterLetter> Filters { get; set; }

        public IEnumerable<SortLetter> Sorts { get; set; }
    }

    public class Settings
    {
        /// <summary>
        /// Откуда получать письма
        /// </summary>
        public LettersFrom LettersStorage { get; set; }

        /// <summary>
        /// Получать ли вложения
        /// </summary>
        public NeedAttachments NeedAttachments { get; set; }
    }

    public class FilterLetter
    {
        /// <summary>
        /// Поля контракта письма
        /// </summary>
        public LetterField Field { get; set; }

        /// <summary>
        /// Шаблон. Кроме Text и Attacments!
        /// </summary>
        public string Template { get; set; }

        /// <summary>
        /// Шаблоны. Только для Text и Attacments!
        /// </summary>
        public IEnumerable<string> Templates { get; set; }

        public bool isRegex { get; set; }
    }

    public class SortLetter
    {
        public LetterField Field { get; set;}
        public SortBy SortBy { get; set; }

    }

    public enum SortBy
    {
        Asc,
        Desc
    }

    /// <summary>
    /// Поля контракта письма
    /// </summary>
    public enum LetterField
    {
        Sender,
        Receivers,
        Subject,
        Date,
        Text,
        Attacments
    }

    /// <summary>
    /// Откуда получать письма
    /// </summary>
    public enum LettersFrom
    {
        /// <summary>
        /// С почтового сервера
        /// </summary>
        Server,

        /// <summary>
        /// Сохраненные в локальную базу
        /// </summary>
        LocalDb,        

        /// <summary>
        /// С локальной базы и удаленного сервера
        /// </summary>
        LocalAndRemote 
    }

    /// <summary>
    /// Получать ли вложения
    /// </summary>
    public enum NeedAttachments
    {
        /// <summary>
        /// Без вложений
        /// </summary>
        WithoutAttachments,

        /// <summary>
        /// только названия вложений
        /// </summary>
        OnlyName,

        /// <summary>
        /// С вложениями
        /// </summary>
        WithAttachmentsBlob
    }
}
