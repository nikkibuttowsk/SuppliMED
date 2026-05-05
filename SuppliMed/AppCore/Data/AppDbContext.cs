using Microsoft.EntityFrameworkCore;
using AppCore.Models;

namespace AppCore.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<MedicalSupply> Supplies { get; set; }
        public DbSet<Batch> Batches { get; set; }
        public DbSet<AuditLog> AuditLogs { get; set; } 

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<MedicalSupply>()
                .HasDiscriminator<string>("SupplyType")
                .HasValue<Medicine>("Medicine")
                .HasValue<Equipment>("Equipment");

            modelBuilder.Entity<Batch>()
                .HasOne(b => b.MedicalSupply)
                .WithMany(s => s.Batches)
                .HasForeignKey(b => b.MedicalSupplyId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<AuditLog>(entity =>
            {
                entity.ToTable("AuditLogs"); // Explicit Table Name
                entity.HasKey(t => t.LogId);
                entity.Property(t => t.Action).IsRequired();
                entity.Property(t => t.Item).IsRequired();
            });
        }
    }
}