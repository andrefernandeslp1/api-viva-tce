using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.SignalR;

namespace API.Models;

[Table("Servicos")]
public class Servico
{
    [Key]
    public int Id {get; set;}

    [Required]
    public string Nome {get; set;}

    public string Descricao {get; set;}

    public decimal Preco {get; set;}

    public string Imagens {get; set;}

    [ForeignKey("FornecedorId")]
    public int FornecedorId {get; set;}
    public Fornecedor Fornecedor {get; set;}

}