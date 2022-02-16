using System.ComponentModel.DataAnnotations;

namespace Iris.Database
{
    public class MailServer
    {
        public int Id { get; set; }
        
        [Required]
        public string Host { get; set; }

        [Required]
        public int Port { get; set; }

        public string Name { get; set; }

        public List<Account> Accounts { get; set; }
    }
}
