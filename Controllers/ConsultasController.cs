using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using VendaDeSementes.Models;

namespace VendaDeSementes.Controllers
{
    public class ConsultasController : Controller
    {
        private readonly Contexto contexto;

        public ConsultasController(Contexto context)
        {
            contexto = context;
        }
       


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult FiltrarProdutos()
        {
            return View();
        }
        public IActionResult ResFiltrarProduto(string? nome, decimal? preco)
        {
            // Inicializa a consulta base
            var consulta = contexto.Produtos.AsQueryable();

            // Filtra por nome se fornecido
            if (!string.IsNullOrEmpty(nome))
            {
                consulta = consulta.Where(p => p.Descricao.Contains(nome));
            }

            // Filtra por preço se fornecido
            if (preco.HasValue)
            {
                consulta = consulta.Where(p => p.Preco == preco.Value);
            }

            // Executa a consulta e converte para lista
            var listaProduto = consulta.ToList();

            return View(listaProduto);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult VendasPivot()
        {
            try
            {
                var pivotVendas = contexto.Vendas
                    .Include(v => v.Produto)
                    .GroupBy(v => new { v.Produto.Descricao, Mes = v.Data.Month })
                    .Select(grupo => new
                    {
                        Produto = grupo.Key.Descricao,
                        Mes = grupo.Key.Mes,
                        ValorTotal = grupo.Sum(v => v.Produto.Preco)
                    })
                    .ToList();

                // Transformação para o formato Pivot
                var resultadoPivot = pivotVendas
                    .GroupBy(x => x.Produto)
                    .Select(grupo => new VendaPivot
                    {
                        Produto = grupo.Key,
                        VendasPorMes = grupo.ToDictionary(v => v.Mes, v => v.ValorTotal)
                    })
                    .ToList();

                return View(resultadoPivot);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Erro ao processar a consulta: {ex.Message}");
                return View(new List<VendaPivot>()); // Retorna lista vazia em caso de erro
            }
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Totalizadores()
        {
            try
            {
                // Consulta agrupando vendas por consumidor e produto
                var resumo = contexto.Vendas
                    .Include(v => v.Consumidor) // Inclui os dados do consumidor
                    .Include(v => v.Produto)   // Inclui os dados do produto
                    .GroupBy(v => new { v.Consumidor.Nome, v.Produto.Descricao }) // Agrupa por consumidor e produto
                    .Select(grupo => new ResumoVenda
                    {
                        Consumidor = grupo.Key.Nome,          // Nome do consumidor
                        Produto = grupo.Key.Descricao,       // Nome do produto
                        QuantidadeTotal = grupo.Count(),     // Quantidade total de vendas
                        ValorTotal = grupo.Sum(v => v.Produto.Preco) // Valor total das vendas
                    })
                    .OrderBy(r => r.Consumidor)
                    .ThenBy(r => r.Produto)
                    .ToList();

                // Soma total global do valor das vendas
                var valorTotalGlobal = resumo.Sum(r => r.ValorTotal);

                // Passa os dados para a View usando ViewBag
                ViewBag.ValorTotalGlobal = valorTotalGlobal;

                // Retorna a View com os dados de resumo
                return View(resumo);
            }
            catch (Exception ex)
            {
                // Trata exceções e retorna uma View vazia
                ModelState.AddModelError(string.Empty, $"Erro ao processar a consulta: {ex.Message}");
                return View(new List<ResumoVenda>()); // Retorna uma lista vazia
            }
        }







    }
}