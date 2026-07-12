using Microsoft.EntityFrameworkCore;

namespace ForgeCore.Models
{
    public class MaterialUnidadeMedida
    {
        public string MaterialId { get; set; }
        public string UnidadeMedidaId {  get; set; }
        public string Numerator { get; set; }
        public string Denominator { get; set; }
        public Material Material { get; set; } = null!;
        public UnidadeMedida UnidadeMedida { get; set; } = null!;
    }
}
