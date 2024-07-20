using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.DTOs;

public class FornecedorDTO
{
   public int Id {get; set;}
   
   [Required]
   public string Nome {get; set;}
   [Required]
   public string Contato {get; set;}
   public string? Logo {get; set;}

}