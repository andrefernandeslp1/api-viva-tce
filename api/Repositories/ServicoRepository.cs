using API.Context;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories;

public class ServicoRepository : RepositoryAsync<Servico>, IServicoRepository
{
    public ServicoRepository(AppDbContext context): base(context)
    {       
    }

     public async Task<IEnumerable<Servico>> GetAllWithFornecedoresAsync()
    {
        return await _context.Servicos.Include(s => s.fornecedor).ToListAsync();
    }

    public async Task<Servico> GetbyIdWithFornecedoresAsync(int id)
    {
        return await _context.Servicos.Include(s => s.fornecedor).FirstOrDefaultAsync(s => s.Id == id);
    }

}