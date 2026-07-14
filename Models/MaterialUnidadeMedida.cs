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
        [Required]
        [Display(Name = "Numerador")]
        public string Numerator { get; set; }
        [Required]
        [Display(Name = "Denominador")]
        public string Denominator { get; set; }
        public Material Material { get; set; } = null!;
        public UnidadeMedida UnidadeMedida { get; set; } = null!;
    }
}
