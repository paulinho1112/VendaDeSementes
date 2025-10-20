using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VendaDeSementes.Models
{
    [Table("Sementes")]
    public class Semente
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [Required(ErrorMessage = "Campo descrição é obrigatório")]
        [StringLength(35)]
        [Display(Name = "Descrição: ")]
        public string descricao { get; set; }
    }
}
