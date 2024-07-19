using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models;

[Table("ServicosUsuarios")]
public class ServicoUsuario
{
    [Key]
    public int Id { get; set; }
    
    public int ServicoId { get; set; }

    [ForeignKey("ServicoId")]
    public Servico servico {get; set;}
    
    public int UsuarioId { get; set; }

    [ForeignKey("UsuarioId")]
    public Usuario usuario {get; set;}
    
    [Required]
    public DateTime Data { get; set; }
    
    [Required]
    public bool Assinatura { get; set; }

}