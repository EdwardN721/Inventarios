using Inventarios.Entities.Models;
using Inventarios.Infrastructure.Context;
using Inventarios.Business.Interface.Repository;

namespace Inventarios.Infrastructure.Repository;

public class CategoriasRepository : BaseRepository<Categoria>, ICategoriasRepository
    
{
    public CategoriasRepository(InventariosDbContext context) : base(context)
    {
        
    }
}