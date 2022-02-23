using System.ComponentModel.DataAnnotations;

namespace Iris.Database
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Password { get; set; }

        public bool IsAdmin { get; set; }

        public List<Account> Accounts { get; set; }
    }
}
