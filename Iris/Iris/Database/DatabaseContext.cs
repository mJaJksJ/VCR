﻿using Microsoft.EntityFrameworkCore;

namespace Iris.Database
{
#pragma warning disable 1591
    public class DatabaseContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<MailServer> MailServers { get; set; }

        public DbSet<AuthRequestOperation> AuthRequests { get; set; }

        public DbSet<Letter> Letters { get; set; }
        public DbSet<Attachment> Attachments { get; set; }
        public DbSet<Person> Persons { get; set; }

        private readonly string _connectionString = "Data Source=Database\\Database.db";

        public DatabaseContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(_connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Letter>()
                    .HasMany(c => c.Receivers)
                    .WithMany(s => s.ReceivedLetters);

            modelBuilder.Entity<Person>()
                .HasMany(c => c.SentLetters)
                .WithOne(s => s.Sender);
        }
    }
}
