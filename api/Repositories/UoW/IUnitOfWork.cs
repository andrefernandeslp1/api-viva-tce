namespace API.Repositories;

public interface IUnitOfWork
{
    IUsuarioRepository UsuarioRepository { get; }
    IFornecedorRepository FornecedorRepository { get; }
    IServicoRepository ServicoRepository { get; }
    IServicoUsuarioRepository ServicoUsuarioRepository { get; }
    void Commit();
}
