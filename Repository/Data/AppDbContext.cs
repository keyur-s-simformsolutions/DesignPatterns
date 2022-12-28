using Microsoft.EntityFrameworkCore;
using Repository.Data.Entity;

namespace Repository.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> dbContextOptions) : base(dbContextOptions)
        {
        }
        public DbSet<Item> Items { get; set; }
        public DbSet<Category> Category { get; set; }

    }
}
