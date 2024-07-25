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

    public async Task<List<Servico>> GetServicosByFornecedorId(int id)
    {
        return await _context.Servicos.Where(f => f.FornecedorId == id).ToListAsync();
    }

    public PagedList<Servico> GetServicosPaginados(PaginationParameters paginationParameters)
    {
       var servicos = _context.Servicos.OrderBy(p => p.Id).AsQueryable();
       var servicosOrdenados = PagedList<Servico>.ToPagedList(servicos, paginationParameters.PageNumber, paginationParameters.PageSize);
       return servicosOrdenados;
    }

    public PagedList<Servico> GetServicosFiltroNome(NomeFilter nomeFilter)
    {
        var servicos = _context.Servicos.AsQueryable();
        servicos = servicos.Where(p => p.Nome.ToUpper().Contains( nomeFilter.nome.ToUpper()));

        var usuariosFiltrados = PagedList<Servico>.ToPagedList(servicos,  nomeFilter.PageNumber,  nomeFilter.PageSize);

        return usuariosFiltrados;
    }
}

