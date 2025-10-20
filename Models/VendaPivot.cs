using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VendaDeSementes.Models
{
    public class VendaPivot
    {
        [Key]
        public string Produto { get; set; }

        [NotMapped] // Informa ao Entity Framework que esta propriedade não será mapeada no banco de dados
        public Dictionary<int, decimal> VendasPorMes { get; set; }
    }
}
