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
    }
}
