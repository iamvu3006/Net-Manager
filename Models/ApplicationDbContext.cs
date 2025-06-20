using Microsoft.EntityFrameworkCore;

namespace MvcMovie.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Computer> Computers { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UsageHistory> UsageHistories { get; set; }
    }
}
