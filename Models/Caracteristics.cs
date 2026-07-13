using System.ComponentModel.DataAnnotations;

namespace ForgeCore.Models
{
    public class Caracteristics
    {
        [Required]
        [Display(Name = "Característica")]
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required]
        [Display(Name = "Nome")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Valor")]
        public string Value { get; set; }
        [Required]
        [Display(Name = "Material")]
        public string MaterialId { get; set; }
        public Material Material { get; set; } = null!;
    }
}
