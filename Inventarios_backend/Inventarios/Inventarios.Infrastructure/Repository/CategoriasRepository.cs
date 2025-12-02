using Inventarios.Bussiness.Interface.Repository;
using Inventarios.Entities.Models;
using Inventarios.Infrastructure.Context;

namespace Inventarios.Infrastructure.Repository;

public class CategoriasRepository : BaseRepository<Categoria>, ICategoriasRepository
    
{
    public CategoriasRepository(InventariosDbContext context) : base(context)
    {
        
    }
}