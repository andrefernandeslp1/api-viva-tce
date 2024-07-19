namespace API.Repositories;

public interface IUnitOfWork
{
    IUsuarioRepository UsuarioRepository { get; }
    void Commit();
}
