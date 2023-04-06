using System.Data.Entity;

namespace UNP.Models
{
    public class UserContext : DbContext
    {
        public DbSet<User> Users { get; set; }
    }
}