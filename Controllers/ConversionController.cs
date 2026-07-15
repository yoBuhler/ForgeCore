using ForgeCore.Services;
using ForgeCore.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ForgeCore.Controllers
{
    [Route("Conversion")]
    public class ConversionController : Controller
    {
        private readonly IMaterialService _materialService;
        private readonly IConversorService _conversorService;

        public ConversionController(IMaterialService materialService, IConversorService conversorService)
        {
            _materialService = materialService;
            _conversorService = conversorService;
        }

        // GET: /Conversion
        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            var materiais = await _materialService.GetAllMaterialAsync();
            ViewBag.Materiais = materiais;
            return View();
        }

        // GET: /Conversion/GetMeasurementUnitByMaterialIdAsync/5 -> Retorna JSON das unidades do material
        [HttpGet("GetMeasurementUnitByMaterialIdAsync/{id}")]
        public async Task<IActionResult> GetMeasurementUnitByMaterialIdAsync(string id)
        {
            var measurementUnits = await _materialService.GetMeasurementUnitByMaterialIdAsync(id);
            return Json(measurementUnits);
        }

        // POST: /Conversion/Converter -> Processa a conversão e retorna JSON
        [HttpPost("Converter")]
        public async Task<IActionResult> Converter([FromBody] RequestConversionCalcViewModel request)
        {
            if (!ModelState.IsValid)
                return BadRequest("Dados inválidos");

            var response = await _conversorService.ConverterAsync(request);
            return Json(response);
        }
    }
}
