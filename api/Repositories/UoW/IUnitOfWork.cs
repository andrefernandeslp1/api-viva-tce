namespace API.Repositories;

public interface IUnitOfWork
{
    IUsuarioRepository UsuarioRepository { get; }
    IFornecedorRepository FornecedorRepository {get; }
    void Commit();
}
