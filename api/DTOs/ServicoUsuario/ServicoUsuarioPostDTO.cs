using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models;

public class ServicoUsuarioPostDTO
{
    public int Id { get; set; }
    public int ServicoId { get; set; }
    public int UsuarioId { get; set; }
    public DateTime Data { get; set; }
    public bool Assinatura { get; set; }

}