using API.Context;

namespace API.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private IUsuarioRepository? _userRepo;

    public AppDbContext _context;
    public UnitOfWork(AppDbContext context)
    {
        _context = context;
    }

    public IUsuarioRepository UsuarioRepository
    {
        get
        {
            return _userRepo = _userRepo ?? new UsuarioRepository(_context);
            //if (_userRepo == null)
            //{
            //    _userRepo = new UsuarioRepository(_context);
            //}
            //return _userRepo;
        }
    }

    public void Commit()
    {
        _context.SaveChanges();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
