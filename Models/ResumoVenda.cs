using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;


namespace VendaDeSementes.Models
{ 
    public class ResumoVenda
    {
       [Key]
        public string Consumidor { get; set; }
        public string Produto { get; set; }    
        public int QuantidadeTotal { get; set; } 
        public decimal ValorTotal { get; set; } 
    }
}
