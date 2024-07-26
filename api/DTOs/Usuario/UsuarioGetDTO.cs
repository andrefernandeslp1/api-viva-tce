using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using API.Models;

namespace API.DTOs;

public class UsuarioGetDTO
{
    public int Id { get; set; }
    
    [Required]
    [StringLength(100)]
    public string Nome { get; set; }
    
    [Required]
    [StringLength(100)]
    [EmailAddress]
    public string Email { get; set; }
    
    [StringLength(200)]
    public string Telefone { get; set; }
    
    [Required]
    public string Role { get; set;}

    public int? IdFornecedor { get; set; }

    public Fornecedor? Fornecedor { get; set; }


}