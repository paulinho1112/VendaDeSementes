using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VendaDeSementes.Models
{
    [Table("Consumidor")]
    public class Consumidor
    {
        [Key]
        [Display(Name = "ID: ")]
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório")] 
        [Display(Name = "Nome: ")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O endereço é obrigatório")]
        [StringLength(200, ErrorMessage = "O endereço deve ter no máximo 200 caracteres")]
        [Display(Name = "Endereço: ")]
        public string Endereco { get; set; }

        [Required(ErrorMessage = "O email é obrigatório")] 
        [EmailAddress(ErrorMessage = "Formato de email inválido")]
        [Display(Name = "Email: ")]
        public string Email { get; set; }
    }
}
