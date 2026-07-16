
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using ForgeCore.Models;
using ForgeCore.Data;
using ForgeCore.ViewModels;
using ForgeCore.Migrations;

public class MaterialController : Controller
{
    private readonly AplicationDbContext _context;

    public MaterialController(AplicationDbContext context)
    {
        _context = context;
    }

    // GET: MATERIALS
    public async Task<IActionResult> Index()    
    {
        return View(await _context.Materials.ToListAsync());
    }

    // GET: MATERIALS/Details/5
    public async Task<IActionResult> Details(string? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var material = await _context.Materials
            .Include(c => c.Caracteristics)
            .Include(mum => mum.MaterialUnidades)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (material == null)
        {
            return NotFound();
        }

        List<UnidadeMedida> unidadeMedidas = _context.UnidadesMedida.ToList();

        foreach (UnidadeMedida unidadeMedida in unidadeMedidas)
        {
            if (!material.MaterialUnidades.Exists(mum => mum.UnidadeMedidaId == unidadeMedida.Id))
            {
                MaterialUnidadeMedida materialUnidadeMedida = new MaterialUnidadeMedida();
                materialUnidadeMedida.MaterialId = id;
                materialUnidadeMedida.UnidadeMedidaId = unidadeMedida.Id;
                if (unidadeMedida.Id == material.UnidadeBaseId)
                {
                    materialUnidadeMedida.Numerator = 1;
                    materialUnidadeMedida.Denominator = 1;
                }
                material.MaterialUnidades.Add(materialUnidadeMedida);
            }
        }

        var model = new MaterialCreateViewModel
        {
            Id = material.Id,
            Name = material.Name,
            UnidadeBaseId = material.UnidadeBaseId,
            Caracteristics = material.Caracteristics,
            MaterialUnidadeMedida = material.MaterialUnidades
        };
        return View(model);
    }

    // GET: MATERIALS/Create
    public IActionResult Create()
    {
        var model = new MaterialCreateViewModel
        {
            UnidadeBaseOptions = _context.UnidadesMedida.Select(um => new SelectListItem
            {
                Value = um.Id,
                Text = $"{um.Id} - {um.Nome}"
            }).ToList(),
            MaterialUnidadeMedida = _context.UnidadesMedida.Select(um => new MaterialUnidadeMedida
            {
                UnidadeMedidaId = um.Id,
            }).ToList()
        };
        model.Caracteristics.Add(new Caracteristics());
        return View(model);
    }

    // POST: MATERIALS/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(MaterialCreateViewModel material)
    {
        if (ModelState.IsValid)
        {
            material.Caracteristics.RemoveAll(c => string.IsNullOrWhiteSpace(c.Name) || string.IsNullOrWhiteSpace(c.Value));
            material.MaterialUnidadeMedida.RemoveAll(mum => (mum.Numerator == null || mum.Numerator <= 0) || (mum.Denominator == null || mum.Denominator <= 0));

            var newMaterial = new Material
            {
                Id = material.Id,
                Name = material.Name,
                UnidadeBaseId = material.UnidadeBaseId,
                Caracteristics = material.Caracteristics,
                MaterialUnidades = material.MaterialUnidadeMedida
            };
            _context.Add(newMaterial);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        material.UnidadeBaseOptions = _context.UnidadesMedida.Select(um => new SelectListItem
        {
            Value = um.Id,
            Text = $"{um.Id} - {um.Nome}"
        }).ToList();

        return View(material);
    }

    // GET: MATERIALS/Edit/5
    public async Task<IActionResult> Edit(string? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var material = await _context.Materials
            .Include(c => c.Caracteristics)
            .Include(mum => mum.MaterialUnidades)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (material == null)
        {
            return NotFound();
        }

        if (material.Caracteristics.Count() == 0)
        {
            material.Caracteristics.Add(new Caracteristics());
        }
        List<UnidadeMedida> unidadeMedidas = _context.UnidadesMedida.ToList();

        foreach (UnidadeMedida unidadeMedida in unidadeMedidas)
        {
            if (!material.MaterialUnidades.Exists(mum => mum.UnidadeMedidaId == unidadeMedida.Id))
            {
                MaterialUnidadeMedida materialUnidadeMedida = new MaterialUnidadeMedida();
                materialUnidadeMedida.MaterialId = id;
                materialUnidadeMedida.UnidadeMedidaId = unidadeMedida.Id;
                if (unidadeMedida.Id == material.UnidadeBaseId)
                {
                    materialUnidadeMedida.Numerator = 1;
                    materialUnidadeMedida.Denominator = 1;
                }
                material.MaterialUnidades.Add(materialUnidadeMedida);
            }
        }

        var model = new MaterialCreateViewModel
        {
            Id = material.Id,
            Name = material.Name,
            UnidadeBaseId = material.UnidadeBaseId,
            Caracteristics = material.Caracteristics,
            MaterialUnidadeMedida = material.MaterialUnidades,
            UnidadeBaseOptions = _context.UnidadesMedida.Select(um => new SelectListItem
            {
                Value = um.Id,
                Text = $"{um.Id} - {um.Nome}"
            }).ToList()
        };
        return View(model);
    }

    // POST: MATERIALS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(string? id, MaterialCreateViewModel material)
    {
        if (id != material.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            var existingMaterial = await _context.Materials
                .Include(m => m.Caracteristics)
                .Include(m => m.MaterialUnidades)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (existingMaterial == null)
            {
                return NotFound();
            }

            try
            {
                material.Caracteristics.RemoveAll(c => string.IsNullOrWhiteSpace(c.Name) || string.IsNullOrWhiteSpace(c.Value));
                material.MaterialUnidadeMedida.RemoveAll(mum => (mum.Numerator == null || mum.Numerator <= 0) || (mum.Denominator == null || mum.Denominator <= 0));

                existingMaterial.Name = material.Name;
                existingMaterial.UnidadeBaseId = material.UnidadeBaseId;

                var incomingCaracIds = material.Caracteristics.Select(c => c.Id).ToList();
                existingMaterial.Caracteristics.RemoveAll(c => !incomingCaracIds.Contains(c.Id));

                // Atualiza as existentes ou adiciona as novas
                foreach (var incomingCarac in material.Caracteristics)
                {
                    var existingCarac = existingMaterial.Caracteristics
                        .FirstOrDefault(c => c.Id == incomingCarac.Id && c.Id != default);

                    if (existingCarac != null)
                    {
                        // Atualiza os valores da característica que já existia
                        existingCarac.Name = incomingCarac.Name;
                        existingCarac.Value = incomingCarac.Value;
                    }
                    else
                    {
                        // Adiciona como nova característica
                        existingMaterial.Caracteristics.Add(incomingCarac);
                    }
                }

                var incomingUnidadeIds = material.MaterialUnidadeMedida.Select(u => u.UnidadeMedidaId).ToList();
                existingMaterial.MaterialUnidades.RemoveAll(u => !incomingUnidadeIds.Contains(u.UnidadeMedidaId));

                // Atualiza as existentes ou adiciona as novas
                foreach (var incomingUnidade in material.MaterialUnidadeMedida)
                {
                    var existingUnidade = existingMaterial.MaterialUnidades
                        .FirstOrDefault(u => u.UnidadeMedidaId == incomingUnidade.UnidadeMedidaId);

                    if (existingUnidade != null)
                    {
                        existingUnidade.Numerator = incomingUnidade.Numerator;
                        existingUnidade.Denominator = incomingUnidade.Denominator;
                    }
                    else
                    {
                        existingMaterial.MaterialUnidades.Add(incomingUnidade);
                    }
                }

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MaterialExists(material.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }

        material.UnidadeBaseOptions = _context.UnidadesMedida.Select(um => new SelectListItem
        {
            Value = um.Id,
            Text = $"{um.Id} - {um.Nome}"
        }).ToList();

        return View(material);
    }

    // GET: MATERIALS/Delete/5
    public async Task<IActionResult> Delete(string? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var material = await _context.Materials
            .FirstOrDefaultAsync(m => m.Id == id);
        if (material == null)
        {
            return NotFound();
        }

        return View(material);
    }

    // POST: MATERIALS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(string? id)
    {
        var material = await _context.Materials.FindAsync(id);
        if (material != null)
        {
            _context.Materials.Remove(material);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool MaterialExists(string? id)
    {
        return _context.Materials.Any(e => e.Id == id);
    }
}
