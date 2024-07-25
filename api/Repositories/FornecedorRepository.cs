using API.Context;
using API.Models;

namespace API.Repositories;

public class FornecedorRepository : RepositoryAsync<Fornecedor>, IFornecedorRepository
{
    public FornecedorRepository(AppDbContext context): base(context)
    {       
    }

    
    public PagedList<Fornecedor> GetFornecedoresPaginados(PaginationParameters paginationParameters)
    {
       var fornecedores = _context.Fornecedores.OrderBy(p => p.Id).AsQueryable();
       var fornecedoresOrdenados = PagedList<Fornecedor>.ToPagedList(fornecedores, paginationParameters.PageNumber, paginationParameters.PageSize);
       return fornecedoresOrdenados;
    }

    public PagedList<Fornecedor> GetFornecedoresFiltroNome(NomeFilter nomeFilter)
    {
        var fornecedores = _context.Fornecedores.AsQueryable();
        fornecedores = fornecedores.Where(p => p.Nome.ToUpper().Contains( nomeFilter.nome.ToUpper()));

        var fornecedoresFiltrados = PagedList<Fornecedor>.ToPagedList(fornecedores,  nomeFilter.PageNumber,  nomeFilter.PageSize);

        return fornecedoresFiltrados;
    }

}