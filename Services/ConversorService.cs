using ForgeCore.Data;
using ForgeCore.Models;
using ForgeCore.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace ForgeCore.Services
{
    public interface IConversorService
    {
        Task<ResponseConversionCalcViewModel> ConverterAsync(RequestConversionCalcViewModel request);
    }

    public class ConversorService : IConversorService
    {
        private readonly AplicationDbContext _context;

        public ConversorService(AplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseConversionCalcViewModel> ConverterAsync(RequestConversionCalcViewModel request)
        {
            var response = new ResponseConversionCalcViewModel
            {
                Success = false
            };

            var material = await _context.Materials
                .FirstOrDefaultAsync(m => m.Id == request.MaterialId);

            if (material == null)
            {
                response.Message = "Não foi encontrado o material especificado.";
                return response;
            }

            // Se a unidade de origem for igual a de destino, não precisa calcular
            if (request.UnitFromId == request.UnitToId)
            {
                response.ConvertedValue = request.Value;
                response.Success = true;
                return response;
            }

            // Consulta todas as conversões
            var conversions = await _context.MaterialsUnidadesMedida
                .Where(mum => mum.MaterialId == request.MaterialId)
                .ToListAsync();

            if (request.UnitFromId != material.UnidadeBaseId && !conversions.Exists(mum => mum.UnidadeMedidaId == request.UnitFromId))
            {
                response.Message = "Não foi encontrado a unidade de medida DE no cadastro do material.";
                return response;
            }

            if (request.UnitToId != material.UnidadeBaseId && !conversions.Exists(mum => mum.UnidadeMedidaId == request.UnitToId))
            {
                response.Message = "Não foi encontrado a unidade de medida PARA no cadastro do material.";
                return response;
            }

            response.ConvertedValue = request.Value;
            response.Success = true;

            // Converte quantidade de unidade de medida DE para unidade de medida base
            if (request.UnitFromId != material.UnidadeBaseId)
            {
                var conversion = conversions.Find(mum => mum.UnidadeMedidaId == request.UnitFromId);
                response.ConvertedValue = (decimal)(response.ConvertedValue * conversion.Denominator / conversion.Numerator);
            }

            // Converte quantidade de unidade de medida PARA para unidade de medida base
            if (request.UnitToId != material.UnidadeBaseId)
            {
                var conversion = conversions.Find(mum => mum.UnidadeMedidaId == request.UnitToId);
                response.ConvertedValue = (decimal)(response.ConvertedValue * conversion.Numerator / conversion.Denominator);
            }

            return response;
        }
    }
}
