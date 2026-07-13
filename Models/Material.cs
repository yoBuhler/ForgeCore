using System.ComponentModel.DataAnnotations;

namespace ForgeCore.Models
{
    public class Material
    {
        [Required]
        [Display(Name = "Material")]
        public string Id { get; set; }
        [Required]
        [Display(Name = "Descrição")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "UM básica")]
        public string UnidadeBaseId { get; set; }
        public List<Caracteristics> Caracteristics { get; set; } = new List<Caracteristics>();
        public List<MaterialUnidadeMedida> MaterialUnidades { get; set; } = new List<MaterialUnidadeMedida>();
    }
}
