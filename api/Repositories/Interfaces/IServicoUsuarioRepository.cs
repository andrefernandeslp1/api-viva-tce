using API.Models;

namespace API.Repositories;

public interface IServicoUsuarioRepository : IRepositoryAsync<ServicoUsuario>
{
    public Task<List<ServicoUsuario>> GetAllWithDataAsync();
    public Task<ServicoUsuario> GetByIdWithDataAsync(int id);
}
