using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models;

[Table("Fornecedores")]
public class Fornecedor
{
   [Key]
   public int Id {get; set;}
   
   [Required]
   public string Nome {get; set;}
   [Required]
   public string Contato {get; set;}
   public string? Logo {get; set;}

}