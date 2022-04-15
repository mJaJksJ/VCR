namespace Iris.Services.LettersService.Contracts
{
    /// <summary>
    /// Котракт вложения
    /// </summary>
    public class AttachmentContract
    {
        /// <summary>
        /// Id вложения
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Название
        /// </summary>
        public string Name { get; set; }
    }
}
