using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using VendaDeSementes.Models;

namespace VendaDeSementes.Controllers
{
    public class VendasController : Controller
    {
        private readonly Contexto _context;

        public VendasController(Contexto context)
        {
            _context = context;
        }

        [Authorize(Roles = "Admin")]
        // GET: Vendas
        public async Task<IActionResult> Index()
        {
            var vendas = _context.Vendas
              .Include(v => v.Produto)
              .Include(v => v.Consumidor);    
            return View(await vendas.ToListAsync());
        }

        // GET: Vendas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var venda = await _context.Vendas             
              .Include(v => v.Produto)
              .Include(v => v.Consumidor)   
                .FirstOrDefaultAsync(m => m.Id == id);
            if (venda == null)
            {
                return NotFound();
            }

            return View(venda);
        }

        // GET: Vendas/Create
        public IActionResult Create()
        {
            ViewBag.Produtos = new SelectList(_context.Produtos, "id", "Descricao");
            ViewBag.Consumidores = new SelectList(_context.Consumidores, "Id", "Nome");
            return View();
        }

        // POST: Vendas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Create([Bind("ProdutoID, ConsumidorID,Data")] Venda venda)
        {

            if (venda.ProdutoID != null && venda.ConsumidorID != null && venda.Data != null)
            {
                _context.Add(venda);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Produtos = new SelectList(_context.Produtos, "id", "Descricao", venda.ProdutoID);
            ViewBag.Consumidores = new SelectList(_context.Consumidores, "Id", "Nome", venda.ConsumidorID);
            return View(venda);
        }

        // GET: Vendas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var venda = await _context.Vendas.FindAsync(id);
            if (venda == null)
            {
                return NotFound();
            }
            ViewBag.Produtos = new SelectList(_context.Produtos, "id", "Descricao", venda.ProdutoID);
            ViewBag.Consumidores = new SelectList(_context.Consumidores, "Id", "Nome", venda.ConsumidorID);
            return View(venda);
        }

        // POST: Vendas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
       
        public async Task<IActionResult> Edit(int id, [Bind("ProdutoID, ConsumidorID,Data")] Venda venda)
        {
            if (id != venda.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(venda);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VendaExists(venda.Id))
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
            ViewBag.Produtos = new SelectList(_context.Produtos, "id", "Descricao", venda.ProdutoID);
            ViewBag.Consumidores = new SelectList(_context.Consumidores, "Id", "Nome", venda.ConsumidorID);
            return View(venda);
        }

        // GET: Vendas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var venda = await _context.Vendas
                 .Include(v => v.Produto)
                 .Include(v => v.Consumidor)
                   .FirstOrDefaultAsync(m => m.Id == id);
            if (venda == null)
            {
                return NotFound();
            }

            return View(venda);
        }

        // POST: Vendas/Delete/5
        [HttpPost, ActionName("Delete")]
       
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var venda = await _context.Vendas.FindAsync(id);
            if (venda != null)
            {
                _context.Vendas.Remove(venda);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VendaExists(int id)
        {
            return _context.Vendas.Any(e => e.Id == id);
        }
    }
}
