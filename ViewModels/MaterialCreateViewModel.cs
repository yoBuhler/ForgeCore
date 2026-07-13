using ForgeCore.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ForgeCore.ViewModels
{
    public class MaterialCreateViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string UnidadeBaseId { get; set; }
        [ValidateNever]
        public List<Caracteristics> Caracteristics { get; set; } = new List<Caracteristics>();
        [ValidateNever]
        public IEnumerable<SelectListItem> UnidadeBaseOptions { get; set; }
    }
}