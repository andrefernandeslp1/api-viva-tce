using API.Context;
using API.Models;

namespace API.Repositories;

public class FornecedorRepository : Repository<Fornecedor>, IFornecedorRepository
{
    public FornecedorRepository(AppDbContext context): base(context)
    {       
    }

}