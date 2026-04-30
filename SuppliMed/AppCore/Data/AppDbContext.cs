using Microsoft.EntityFrameworkCore;
using AppCore.Models;

namespace AppCore.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<MedicalSupply> Supplies { get; set; }
        public DbSet<Batch> Batches { get; set; }
        public DbSet<Transaction> Transactions { get; set; } 

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // TPH Mapping
            modelBuilder.Entity<MedicalSupply>()
                .HasDiscriminator<string>("SupplyType")
                .HasValue<Medicine>("Medicine")
                .HasValue<Equipment>("Equipment");

            // Relationship - This now works because MedicalSupplyId and MedicalSupply.Id are both strings
            modelBuilder.Entity<Batch>()
                .HasOne(b => b.MedicalSupply)
                .WithMany(s => s.Batches)
                .HasForeignKey(b => b.MedicalSupplyId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Transaction>(entity =>
            {
                entity.HasKey(t => t.LogId); // Defines LogId as Primary Key
                entity.Property(t => t.Action).IsRequired();
                entity.Property(t => t.Item).IsRequired();
            });
        }
    }
}