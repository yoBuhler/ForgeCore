using System.ComponentModel.DataAnnotations;

namespace ForgeCore.Models
{
    public class MaterialUnidadeMedida
    {
        [Required]
        [Display(Name = "Material")]
        public string MaterialId { get; set; }
        [Required]
        [Display(Name = "UM")]
        public string UnidadeMedidaId {  get; set; }
        [Display(Name = "Numerador")]
        public int? Numerator { get; set; }
        [Display(Name = "Denominador")]
        public int? Denominator { get; set; }
        public Material Material { get; set; } = null!;
        public UnidadeMedida UnidadeMedida { get; set; } = null!;
    }
}
