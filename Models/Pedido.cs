using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VendaDeSementes.Models
{
    [Table("Pedido")]
    public class Pedido
    {
        [Key] // Define a chave primária
        [Display(Name = "ID: ")]
        public int ID { get; set; }

        [Required(ErrorMessage = "O ConsumidorID é obrigatório")]
        [Display(Name = "ConsumidorID: ")]
        public int ConsumidorID { get; set; } 

        [Required(ErrorMessage = "A Data do pedido é obrigatória")]
        [DataType(DataType.Date, ErrorMessage = "Formato de data inválido")]
        [Display(Name = "Data do pedido: ")]
        public DateTime DataPedido { get; set; } 

        [Required(ErrorMessage = "O Preço é obrigatório")]
       
        [Display(Name = "Preço: ")]
        public decimal Preco { get; set; }

        [Required(ErrorMessage = "A Quantidade é obrigatória")]
        [Display(Name = "Quantidade: ")]
        public int Quantidade { get; set; }
    }
}
