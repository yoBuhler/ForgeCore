using Microsoft.EntityFrameworkCore;

namespace ForgeCore.Models
{
    [Keyless]
    public class MaterialUnidadeMedida
    {
        public string MaterialId { get; set; }
        public string UnidadeMedida {  get; set; }
        public string Numerator { get; set; }
        public string Denominator { get; set; }
    }
}
