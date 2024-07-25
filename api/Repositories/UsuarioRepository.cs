using API.Context;
using API.Models;

namespace API.Repositories;

public class UsuarioRepository : RepositoryAsync<Usuario>, IUsuarioRepository
{
    public UsuarioRepository(AppDbContext context): base(context)
    {       
    }

    
    public PagedList<Usuario> GetUsuariosPaginados(PaginationParameters paginationParameters)
    {
       var usuarios = _context.Usuarios.OrderBy(p => p.Id).AsQueryable();
       var usuariosOrdenados = PagedList<Usuario>.ToPagedList(usuarios, paginationParameters.PageNumber, paginationParameters.PageSize);
       return usuariosOrdenados;
    }

    public PagedList<Usuario> GetUsuariosFiltroNome(NomeFilter nomeFilter)
    {
        var usuarios = _context.Usuarios.AsQueryable();
        usuarios = usuarios.Where(p => p.Nome.ToUpper().Contains( nomeFilter.nome.ToUpper()));

        var usuariosFiltrados = PagedList<Usuario>.ToPagedList(usuarios,  nomeFilter.PageNumber,  nomeFilter.PageSize);

        return usuariosFiltrados;
    }

}