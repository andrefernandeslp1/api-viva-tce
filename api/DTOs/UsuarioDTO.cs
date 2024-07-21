using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.DTOs;

public class UsuarioDTO
{
    public int Id { get; set; }
    
    [Required]
    [StringLength(100)]
    public string Nome { get; set; }
    
    [Required]
    [StringLength(100)]
    public string Senha { get; set; }
    
    [Required]
    [StringLength(100)]
    [EmailAddress]
    public string Email { get; set; }
    
    [StringLength(200)]
    public string Telefone { get; set; }
    
    [Required]
    public Role Role {get; set;}


}