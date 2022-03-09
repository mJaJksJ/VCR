using Microsoft.EntityFrameworkCore;

namespace Iris.Database
{
#pragma warning disable 1591
    public class DatabaseContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<MailServer> MailServers { get; set; }

        public DbSet<AuthRequestOperation> AuthRequests { get; set; }

        public DatabaseContext()
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=Database\\Database.db");
        }
    }
}
