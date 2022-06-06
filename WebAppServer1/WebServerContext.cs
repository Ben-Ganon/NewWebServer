using Microsoft.EntityFrameworkCore;
using ServerFreak.Models;
using WebAppServer1.Models;

namespace WebAppServer1
{
    public class WebServerContext : DbContext
    {
        private const string connectionString = "server=localhost;port=3306;database=WebServerDB;user=root;password=1107";
         protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(connectionString, MariaDbServerVersion.AutoDetect(connectionString));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuring the Name property as the primary
            // key of the Items table
            modelBuilder.Entity<UserF>().HasKey(e => e.Username);
        }

        public DbSet<UserF> Users { get; set; }
        public DbSet<Review> Reviews{ get; set; }

        public DbSet<Chat> Chats { get; set; }

        public DbSet<Contact> Contacts { get; set; }

        public DbSet<Message> Messages { get; set; }    
    }
}
