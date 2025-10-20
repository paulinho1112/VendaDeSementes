using Microsoft.EntityFrameworkCore;
using VendaDeSementes.Models;

namespace VendaDeSementes.Models
{
    public class Contexto : DbContext
    {
        public Contexto(DbContextOptions<Contexto> options) : base(options) { }

        public DbSet<Consumidor> Consumidores { get; set; }
        public DbSet<ItemPedido> ItemPedidos { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Semente> Sementess { get; set; }
        public DbSet<Venda> Vendas { get; set; }
        public DbSet<Login> Logins{ get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configura a precisão e escala 
            modelBuilder.Entity<ItemPedido>()
                .Property(p => p.Preco)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Pedido>()
                .Property(p => p.Preco)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Produto>()
                .Property(p => p.Preco)
                .HasColumnType("decimal(18,2)");

            base.OnModelCreating(modelBuilder);
        }
        public DbSet<VendaDeSementes.Models.VendaPivot> VendaPivot { get; set; }
        public DbSet<VendaDeSementes.Models.ResumoVenda> ResumoVenda { get; set; }
        public DbSet<Produtos> Produtos_1 { get; set; }
    }
}