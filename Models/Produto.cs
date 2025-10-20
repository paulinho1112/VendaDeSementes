using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VendaDeSementes.Models
{
   
    [Table("Produto")]
    public class Produto
    {
       

        [Display(Name = "ID: ")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Indica que o valor da propriedade será gerado automaticamente pelo
                                                              // banco de dados durante a inserção de um registro.
        public int id { get; set; }

        
        [Display(Name = "Descrição: ")]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "O preço é obrigatório")]
       
        [Display(Name = "Preço: ")]
        public decimal Preco { get; set; } 

        [Display(Name = "Quantidade: ")]
        public int Quantidade { get; set; }

        [Display(Name = "Semente: ")]
        public Semente semente { get; set; }
        [Display(Name = "Semente: ")]
        public int sementeID { get; set; }

        
    }
}
