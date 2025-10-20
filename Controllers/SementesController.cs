using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VendaDeSementes.Models;

namespace VendaDeSementes.Controllers
{
    public class SementesController : Controller
    {
        private readonly Contexto _context;

        public SementesController(Contexto context)
        {
            _context = context;
        }

        // GET: Sementes
        public async Task<IActionResult> Index()
        {
            return View(await _context.Sementess.ToListAsync());
        }

        // GET: Sementes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var semente = await _context.Sementess
                .FirstOrDefaultAsync(m => m.id == id);
            if (semente == null)
            {
                return NotFound();
            }

            return View(semente);
        }

        // GET: Sementes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Sementes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        
        public async Task<IActionResult> Create([Bind("id,descricao")] Semente semente)
        {
           // if (ModelState.IsValid)
           // {
                _context.Add(semente);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
          //  }
            return View(semente);
        }

        // GET: Sementes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var semente = await _context.Sementess.FindAsync(id);
            if (semente == null)
            {
                return NotFound();
            }
            return View(semente);
        }

        // POST: Sementes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
       
        public async Task<IActionResult> Edit(int id, [Bind("id,descricao")] Semente semente)
        {
            if (id != semente.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(semente);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SementeExists(semente.id))
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
            return View(semente);
        }

        // GET: Sementes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var semente = await _context.Sementess
                .FirstOrDefaultAsync(m => m.id == id);
            if (semente == null)
            {
                return NotFound();
            }

            return View(semente);
        }

        // POST: Sementes/Delete/5
        [HttpPost, ActionName("Delete")]
       
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var semente = await _context.Sementess.FindAsync(id);
            if (semente != null)
            {
                _context.Sementess.Remove(semente);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SementeExists(int id)
        {
            return _context.Sementess.Any(e => e.id == id);
        }
    }
}
