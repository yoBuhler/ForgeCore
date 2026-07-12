namespace ForgeCore.Models
{
    public class Material
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string UnidadeBaseId { get; set; }
        public ICollection<Caracteristics> Caracteristics { get; set; } = new List<Caracteristics>();
        public ICollection<MaterialUnidadeMedida> MaterialUnidades { get; set; } = new List<MaterialUnidadeMedida>();
    }
}
