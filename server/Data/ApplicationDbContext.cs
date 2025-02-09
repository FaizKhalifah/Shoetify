using Microsoft.EntityFrameworkCore;
using server.Models.Entities;

namespace server.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>options):base(options)
        {
            
        }

        public DbSet<Shoe> Shoes { get; set; }
        public DbSet<Factory> Factories { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Konfigurasi Relasi One-to-Many (Satu Factory memiliki banyak Shoes)
            modelBuilder.Entity<Shoe>()
                .HasOne(s => s.Factory)
                .WithMany(f => f.Shoes)
                .HasForeignKey(s => s.FactoryId)
                .OnDelete(DeleteBehavior.Cascade); // Jika Factory dihapus, Shoes-nya ikut terhapus

            base.OnModelCreating(modelBuilder);
        }
    }
}
