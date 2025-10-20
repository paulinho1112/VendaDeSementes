using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VendaDeSementes.Models;

namespace VendaDeSementes.Controllers
{
    public class ConsumidoresController : Controller
    {
        private readonly Contexto _context;

        public ConsumidoresController(Contexto context)
        {
            _context = context;
        }

        // GET: Consumidores
        public async Task<IActionResult> Index()
        {
            return View(await _context.Consumidores.ToListAsync());
        }

        // GET: Consumidores/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var consumidor = await _context.Consumidores
                .FirstOrDefaultAsync(m => m.Id == id);
            if (consumidor == null)
            {
                return NotFound();
            }

            return View(consumidor);
        }

        // GET: Consumidores/Create
        public IActionResult Create()
        {
            ViewData["ConsumidoresId"] = new SelectList(_context.Consumidores, "id", "descricao");
            return View();
        }

        // POST: Consumidores/Create
        [HttpPost]
        public async Task<IActionResult> Create([Bind("Id,Nome,Endereco,Email")] Consumidor consumidor)
        {
            Console.WriteLine("teste3");
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(consumidor);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    Console.WriteLine("teste1");
                    ModelState.AddModelError("", "Verifique os campos preenchidos.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("teste2");
                ModelState.AddModelError("", $"Erro ao tentar criar o consumidor: {ex.Message}");
            }

            return View(consumidor);
        }

        // GET: Consumidores/Edit/5

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var consumidor = await _context.Consumidores.FindAsync(id);
            if (consumidor == null)
            {
                return NotFound();
            }
            return View(consumidor);
        }

        // POST: Consumidores/Edit/5
        [HttpPost]
      
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Endereco,Email")] Consumidor consumidor)
        {
            if (id != consumidor.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(consumidor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ConsumidorExists(consumidor.Id))
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
            return View(consumidor);
        }

        // GET: Consumidores/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var consumidor = await _context.Consumidores
                .FirstOrDefaultAsync(m => m.Id == id);
            if (consumidor == null)
            {
                return NotFound();
            }

            return View(consumidor);
        }

        // POST: Consumidores/Delete/5
        [HttpPost, ActionName("Delete")]
      
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var consumidor = await _context.Consumidores.FindAsync(id);
            if (consumidor != null)
            {
                _context.Consumidores.Remove(consumidor);
            }
            else
            {
                ModelState.AddModelError("", "Erro: Consumidor não encontrado para exclusão.");
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // Verifica se o consumidor existe no banco de dados
        private bool ConsumidorExists(int id)
        {
            return _context.Consumidores.Any(e => e.Id == id);
        }
    }
}
