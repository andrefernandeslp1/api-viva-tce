using API.Context;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories;

public class ServicoUsuarioRepository : RepositoryAsync<ServicoUsuario>, IServicoUsuarioRepository
{
    public ServicoUsuarioRepository(AppDbContext context): base(context)
    {       
    }

    public async Task<List<ServicoUsuario>> GetAllWithDataAsync()
    {
        return await _context.ServicosUsuarios.Include(s => s.Usuario).Include(s => s.Servico).ThenInclude(s => s.fornecedor).ToListAsync();
    }

    public async Task<ServicoUsuario> GetByIdWithDataAsync(int id)
    {
        return await _context.ServicosUsuarios.Include(s => s.Usuario).Include(s => s.Servico).ThenInclude(s => s.fornecedor).FirstOrDefaultAsync(s => s.Id == id);
    }

}