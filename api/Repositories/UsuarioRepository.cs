using System.Linq.Expressions;
using API.Context;
using API.Models;
using Microsoft.EntityFrameworkCore;

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

    public async Task<IEnumerable<Usuario>> GetAllWithFornecedoresAsync()
    {
        return await _context.Usuarios.Include(u => u.Fornecedor).ToListAsync();
    }

    public async Task<Usuario> GetByIdWithFornecedorAsync(int id)
    {
        return await _context.Usuarios.Include(u => u.Fornecedor).FirstOrDefaultAsync(predicate: (u) => u.Id == id);
    }
}