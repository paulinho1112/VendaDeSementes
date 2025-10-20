using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VendaDeSementes.Models
{
    [Table("ItemPedido")]
    public class ItemPedido
    {
        [Key] 
        [Display(Name = "ID: ")]
        public int ID { get; set; }

        [Required(ErrorMessage = "O PedidoID é obrigatório")]
        [Display(Name = "PedidoID: ")]
        public int PedidoID { get; set; }

        [Required(ErrorMessage = "O ProdutoID é obrigatório")]
        [Display(Name = "ProdutoID: ")]
        public int ProdutoID { get; set; }

        [Required(ErrorMessage = "A Quantidade é obrigatória")]

        [Display(Name = "Quantidade: ")]
        public int Quantidade { get; set; }

        [Required(ErrorMessage = "O Preço é obrigatório")]
        [Display(Name = "Preço: ")]
        public decimal Preco { get; set; }
    }
}
