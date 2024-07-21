using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models;

public class ServicoUsuarioGetDTO
{
    public int Id { get; set; }
    public int ServicoId { get; set; }
    public Servico Servico {get; set;}
    public int UsuarioId { get; set; }
    public Usuario Usuario {get; set;}
    public DateTime Data { get; set; }
    public bool Assinatura { get; set; }

}