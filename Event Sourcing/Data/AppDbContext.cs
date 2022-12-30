using Microsoft.EntityFrameworkCore;

namespace Event_Sourcing.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            _ = modelBuilder.Entity<Product>().HasKey(x => x.Id);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            _ = optionsBuilder.UseInMemoryDatabase("EventSourcingDemo");
        }
    }
}
