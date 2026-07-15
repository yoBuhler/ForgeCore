namespace ForgeCore.ViewModels
{
    public class MaterialCalcViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }

    public class UnidadeMedidaCalcViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }

    public class RequestConversionCalcViewModel
    {
        public string MaterialId { get; set; }
        public string UnitFromId { get; set; }
        public string UnitToId { get; set; }
        public decimal Value { get; set; }
    }

    public class ResponseConversionCalcViewModel
    {
        public decimal ConvertedValue { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
    }
}
