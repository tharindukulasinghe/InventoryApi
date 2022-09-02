using Microsoft.EntityFrameworkCore;

namespace InventoryApi.Data
{
    public class DataContext : DbContext
    {

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<Item> Items { get; set; }

        public DbSet<Repair> Repairs { get; set; }

        public DbSet<Summary> Summary { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder
               .Entity<Summary>()
               .ToView("View_Summary")
               .HasNoKey();
        }
    }
}
