using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models;

[Table("ServicosUsuarios")]
public class ServicoUsuario
{
    [Key]
    public int Id { get; set; }
    
    [ForeignKey("ServicoId")]
    public int ServicoId { get; set; }

    public Servico? Servico {get; set;}
    
    [ForeignKey("UsuarioId")]
    public int UsuarioId { get; set; }

    public Usuario? Usuario {get; set;}
    
    [Required]
    public DateTime Data { get; set; }
    
    [Required]
    public bool Assinatura { get; set; }

}