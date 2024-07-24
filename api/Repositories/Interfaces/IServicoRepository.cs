using API.Models;

namespace API.Repositories;

public interface IServicoRepository : IRepositoryAsync<Servico>
{
    public Task<IEnumerable<Servico>> GetAllWithFornecedoresAsync();
    public Task<Servico> GetbyIdWithFornecedoresAsync(int id);
    public Task<List<Servico>> GetServicosByFornecedorId(int id);
}
