
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ForgeCore.Models;
using ForgeCore.Data;

public class UnidadeMedidaController : Controller
{
    private readonly AplicationDbContext _context;

    public UnidadeMedidaController(AplicationDbContext context)
    {
        _context = context;
    }

    // GET: UNIDADEMEDIDA
    public async Task<IActionResult> Index()    
    {
        return View(await _context.UnidadesMedida.ToListAsync());
    }

    // GET: UNIDADEMEDIDA/Details/5
    public async Task<IActionResult> Details(string? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var unidademedida = await _context.UnidadesMedida
            .FirstOrDefaultAsync(m => m.Id == id);
        if (unidademedida == null)
        {
            return NotFound();
        }

        return View(unidademedida);
    }

    // GET: UNIDADEMEDIDA/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: UNIDADEMEDIDA/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Nome")] UnidadeMedida unidademedida)
    {
        if (ModelState.IsValid)
        {
            _context.Add(unidademedida);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(unidademedida);
    }

    // GET: UNIDADEMEDIDA/Edit/5
    public async Task<IActionResult> Edit(string? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var unidademedida = await _context.UnidadesMedida.FindAsync(id);
        if (unidademedida == null)
        {
            return NotFound();
        }
        return View(unidademedida);
    }

    // POST: UNIDADEMEDIDA/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(string? id, [Bind("Id,Nome")] UnidadeMedida unidademedida)
    {
        if (id != unidademedida.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(unidademedida);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UnidadeMedidaExists(unidademedida.Id))
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
        return View(unidademedida);
    }

    // GET: UNIDADEMEDIDA/Delete/5
    public async Task<IActionResult> Delete(string? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var unidademedida = await _context.UnidadesMedida
            .FirstOrDefaultAsync(m => m.Id == id);
        if (unidademedida == null)
        {
            return NotFound();
        }

        return View(unidademedida);
    }

    // POST: UNIDADEMEDIDA/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(string? id)
    {
        var unidademedida = await _context.UnidadesMedida.FindAsync(id);
        if (unidademedida != null)
        {
            _context.UnidadesMedida.Remove(unidademedida);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool UnidadeMedidaExists(string? id)
    {
        return _context.UnidadesMedida.Any(e => e.Id == id);
    }
}
