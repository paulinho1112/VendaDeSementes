using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VendaDeSementes.Models
{
    [Table("Venda")]
    public class Venda
    {
        
        [Key] // Define a chave primária
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ID: ")]
        public int Id { get; set; }

        [Required(ErrorMessage = "O ID do consumidor é obrigatório")]
        [Display(Name = "ConsumidorID: ")]
        [ForeignKey("ConsumidorID")]
        public int ConsumidorID { get; set; }
        
        public Consumidor Consumidor { get; set; }

        [Required(ErrorMessage = "O ID do produto é obrigatório")]
        [Display(Name = "ProdutoID: ")]
        [ForeignKey("ProdutoID")]
        public int ProdutoID { get; set; }
        
        public Produto Produto { get; set; }

        [Required(ErrorMessage = "A data é obrigatória")]
        [DataType(DataType.Date, ErrorMessage = "Formato de data inválido")]
        [Display(Name = "Data: ")]
        public DateTime Data { get; set; }
    }
}
