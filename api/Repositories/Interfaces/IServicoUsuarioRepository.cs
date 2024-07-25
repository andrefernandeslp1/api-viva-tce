using API.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Repositories;

public interface IServicoUsuarioRepository : IRepositoryAsync<ServicoUsuario>
{
    public Task<List<ServicoUsuario>> GetAllWithDataAsync();
    public Task<ServicoUsuario> GetByIdWithDataAsync(int id);
    public Task<List<ServicoUsuario>> GetComprasByUser(int id);
}
