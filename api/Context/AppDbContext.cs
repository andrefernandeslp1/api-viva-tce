using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options ) : base( options )
    {           
    }
    public DbSet<Fornecedor>? Fornecedores { get; set; }
    public DbSet<Servico>? Servicos { get; set; }
    public DbSet<ServicoUsuario> ServicosUsuarios {get; set;}
    public DbSet<Usuario>? Usuarios { get; set; }
}
