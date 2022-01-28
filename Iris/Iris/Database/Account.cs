using System.ComponentModel.DataAnnotations;

namespace Iris.Database
{
    public class Account
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Password { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public int MailServerId { get; set; }
        public MailServer MailServer { get; set; }
    }
}
