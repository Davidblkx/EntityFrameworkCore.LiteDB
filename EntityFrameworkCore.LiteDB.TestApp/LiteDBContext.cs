using EntityFrameworkCore.LiteDB.Attributes;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkCore.LiteDB.TestApp
{
    public class LiteDBContext : DbContext
    {
        private const string connectionString = "database.db";

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLiteDB(connectionString);
        }

        public DbSet<User> Users { get; set; }
    }

    public class User
    {
        [Field("_id")]
        public string Id { get; set; }

        public string Name { get; set; }
    }
}
