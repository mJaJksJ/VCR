namespace Iris.Api.Controllers.ConnectionsControllers
{
    /// <summary>
    /// Имя почтового сервера
    /// </summary>
    public class MailServerAccountContract
    {
        /// <summary>
        /// Id сервера
        /// </summary>
        public int Id { get; set; }
        
        /// <summary>
        /// Имя сервера
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Id учетной записи
        /// </summary>
        public int AccountId { get; set; }

        /// <summary>
        /// Имя учетной записи
        /// </summary>
        public string AccountName { get; set; }
    }
}
