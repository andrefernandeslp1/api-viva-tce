using System.Linq.Expressions;
using API.Models;

namespace API.Repositories;

public interface IUsuarioRepository : IRepositoryAsync<Usuario>
{
    public PagedList<Usuario> GetUsuariosPaginados(PaginationParameters paginationParameters);
    public PagedList<Usuario> GetUsuariosFiltroNome(NomeFilter nomeFilter);

    public Task<IEnumerable<Usuario>> GetAllWithFornecedoresAsync();
    public Task<Usuario> GetByIdWithFornecedorAsync(int id);
    
}
