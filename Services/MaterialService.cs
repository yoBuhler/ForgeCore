using ForgeCore.Data;
using ForgeCore.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace ForgeCore.Services
{
    public interface IMaterialService
    {
        Task<IEnumerable<MaterialCalcViewModel>> GetAllMaterialAsync();
        Task<IEnumerable<UnidadeMedidaCalcViewModel>> GetMeasurementUnitByMaterialIdAsync(string materialId);
    }

    public class MaterialService : IMaterialService
    {
        private readonly AplicationDbContext _context;

        public MaterialService(AplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MaterialCalcViewModel>> GetAllMaterialAsync()
        {
            return await _context.Materials
                .Select(m => new MaterialCalcViewModel { Id = m.Id, Name = m.Name })
                .ToListAsync();
        }

        public async Task<IEnumerable<UnidadeMedidaCalcViewModel>> GetMeasurementUnitByMaterialIdAsync(string materialId)
        {
            var material = await _context.Materials
                .Include(m => m.MaterialUnidades)
                .FirstOrDefaultAsync(m => m.Id == materialId);

            if (material == null) return Enumerable.Empty<UnidadeMedidaCalcViewModel>();

            var unidades = await _context.UnidadesMedida
                .Select(u => new UnidadeMedidaCalcViewModel
                {
                    Id = u.Id,
                    Name = u.Nome
                })
                .ToListAsync();

            foreach (var u in unidades.ToList())
            {
                if (u.Id != material.UnidadeBaseId && !material.MaterialUnidades.Exists(mum => mum.UnidadeMedidaId == u.Id))
                {
                    unidades.Remove(u);
                }
            }

            return unidades;
        }
    }
}
