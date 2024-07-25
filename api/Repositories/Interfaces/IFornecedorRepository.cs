using API.Models;

namespace API.Repositories;

public interface IFornecedorRepository : IRepositoryAsync<Fornecedor>
{
    public PagedList<Fornecedor> GetFornecedoresPaginados(PaginationParameters paginationParameters);
    public PagedList<Fornecedor> GetFornecedoresFiltroNome(NomeFilter nomeFilter);
}
