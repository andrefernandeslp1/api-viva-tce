using API.Context;

namespace API.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private IUsuarioRepository? _userRepo;
    private IFornecedorRepository? _fornecedorRepo;
    private IServicoRepository? _servicoRepo;
    private IServicoUsuarioRepository? _servicoUsuarioRepo;

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

    public IFornecedorRepository FornecedorRepository
    {
        get
        {
            return _fornecedorRepo = _fornecedorRepo ?? new FornecedorRepository(_context);
        }
    }

    public IServicoRepository ServicoRepository
    {
        get
        {
            return _servicoRepo = _servicoRepo ?? new ServicoRepository(_context);
        }
    }

    public IServicoUsuarioRepository ServicoUsuarioRepository
    {
        get
        {
            return _servicoUsuarioRepo = _servicoUsuarioRepo ?? new ServicoUsuarioRepository(_context);
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
