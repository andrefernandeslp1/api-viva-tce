using API.Context;
using API.Models;

namespace API.Repositories;

public class UsuarioRepository : RepositoryAsync<Usuario>, IUsuarioRepository
{
    public UsuarioRepository(AppDbContext context): base(context)
    {       
    }

}