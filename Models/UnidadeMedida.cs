using System.ComponentModel.DataAnnotations;

namespace ForgeCore.Models
{
    public class UnidadeMedida
    {
        [Required]
        [Display(Name = "Unidade de Medida")]
        public string Id { get; set; }
        [Required]
        [Display(Name = "Descrição")]
        public string Nome { get; set; }
    }
}
