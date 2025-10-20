using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VendaDeSementes.Models;

namespace VendaDeSementes.Controllers
{
    public class ProdutosController : Controller
    {
        private readonly Contexto _context;

        public ProdutosController(Contexto context)
        {
            _context = context;
        }

        // GET: Produtos
        public async Task<IActionResult> Index()
        {
            var contexto = _context.Produtos.Include(p => p.semente);
            return View(await contexto.ToListAsync());
        }

        // GET: Produtos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produto = await _context.Produtos
                .Include(p => p.semente)
                .FirstOrDefaultAsync(m => m.id == id);
            if (produto == null)
            {
                return NotFound();
            }

            return View(produto);
        }

        // GET: Produtos/Create
        public IActionResult Create()
        {
            ViewData["sementeID"] = new SelectList(_context.Sementess, "id", "descricao");
            return View();
        }

        // POST: Produtos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,Descricao,Preco,Quantidade,sementeID")] Produto produto)
        {
         
                _context.Add(produto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
        
            ViewData["sementeID"] = new SelectList(_context.Sementess, "id", "descricao", produto.sementeID);
            return View(produto);
        }


        public IActionResult GerarProdutos()
        {
            _context.Database.ExecuteSqlRaw("delete from Produto");
            _context.Database.ExecuteSqlRaw("DBCC CHECKIDENT ('Produto', RESEED, 0)");
            Random rand = new Random();

            
            string[] nomesProdutos = { "Semente de Girassol", "Semente de Melancia", "Semente de Abóbora", "Semente de Alface", "Semente de Tomate", "Semente de Cenoura", "Semente de Pimenta", "Semente de Espinafre", "Semente de Couve", "Semente de Manjericão" };
            decimal[] precos = { 1.99m, 2.49m, 3.29m, 0.99m, 2.99m, 1.49m, 2.79m, 3.99m, 1.89m, 4.49m };

            // Obter IDs válidos de sementes existentes
            var idsSementesValidos = _context.Sementess.Select(s => s.id).ToList();
            if (!idsSementesValidos.Any())
            {
                return BadRequest("Não existem sementes cadastradas no banco de dados.");
            }

           
          

            for (int i = 0; i < 50; i++)
            {
                var produto = new Produto
                {
                    Descricao = nomesProdutos[i % nomesProdutos.Length],
                    Preco = precos[rand.Next(precos.Length)],
                    Quantidade = rand.Next(900, 5000), 
                    sementeID = idsSementesValidos[rand.Next(idsSementesValidos.Count)] // IDs de sementes válidos
                };

                _context.Produtos.Add(produto);
            }

            
            _context.SaveChanges();

            // Retorna a lista de produtos atualizada para a view
            return View(_context.Produtos.OrderBy(p => p.sementeID).ToList());
        }



        // GET: Produtos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produto = await _context.Produtos.FindAsync(id);
            if (produto == null)
            {
                return NotFound();
            }
            ViewData["sementeID"] = new SelectList(_context.Sementess, "id", "descricao", produto.sementeID);
            return View(produto);
        }

        // POST: Produtos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
       
        public async Task<IActionResult> Edit(int id, [Bind("id,Descricao,Preco,Quantidade,sementeID")] Produto produto)
        {
            if (id != produto.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(produto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProdutoExists(produto.id))
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
            ViewData["sementeID"] = new SelectList(_context.Sementess, "id", "descricao", produto.sementeID);
            return View(produto);
        }

        // GET: Produtos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produto = await _context.Produtos
                .Include(p => p.semente)
                .FirstOrDefaultAsync(m => m.id == id);
            if (produto == null)
            {
                return NotFound();
            }

            return View(produto);
        }

        // POST: Produtos/Delete/5
        [HttpPost, ActionName("Delete")]
       
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var produto = await _context.Produtos.FindAsync(id);
            if (produto != null)
            {
                _context.Produtos.Remove(produto);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProdutoExists(int id)
        {
            return _context.Produtos.Any(e => e.id == id);
        }
    }
}
