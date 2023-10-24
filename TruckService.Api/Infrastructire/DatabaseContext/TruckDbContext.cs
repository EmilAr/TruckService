using Microsoft.EntityFrameworkCore;
using TruckService.Api.Model;

namespace TruckService.Api.Infrastructire.DatabaseContext
{
    public class TruckDbContext : DbContext
    {
        public TruckDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Truck> Trucks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Truck>()
                .HasKey(t => t.Code);

            modelBuilder.Entity<Truck>()
                .Property(t => t.Name)
                .IsRequired();

            modelBuilder.Entity<Truck>()
                .Property(t => t.TruckStatus)
                .IsRequired();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(databaseName: "TruckDb");
        }
    }
}
