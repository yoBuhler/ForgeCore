using ForgeCore.Models;
using Microsoft.EntityFrameworkCore;

namespace ForgeCore.Data
{
    public class AplicationDbContext : DbContext
    {
        public AplicationDbContext(DbContextOptions<AplicationDbContext> options)
        : base(options) { }

        public DbSet<Material> Materials { get; set; }
        public DbSet<UnidadeMedida> UnidadesMedida { get; set; }
        public DbSet<MaterialUnidadeMedida> MaterialsUnidadesMedida { get; set; }
        public DbSet<Caracteristics> Caracteristics { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MaterialUnidadeMedida>()
                .HasKey(m => new { m.MaterialId, m.UnidadeMedidaId });

            modelBuilder.Entity<Material>()
                .HasMany(m => m.Caracteristics)
                .WithOne(m => m.Material)
                .HasForeignKey(m => m.MaterialId)
                .IsRequired();

            modelBuilder.Entity<Material>()
                .HasMany(m => m.MaterialUnidades)
                .WithOne(m => m.Material)
                .HasForeignKey(m => m.MaterialId)
                .IsRequired();
        }
    }
}
