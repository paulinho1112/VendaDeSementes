using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using VendaDeSementes.Models;

[Table("Produtos")]
public class Produtos
{
    [Key]
    public int id { get; set; }
    public string Descricao { get; set; }
    public decimal Preco { get; set; }
    public int Quantidade { get; set; }
    public int sementeID { get; set; }
    public Semente semente { get; set; }
}
