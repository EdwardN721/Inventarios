using Inventarios.Bussiness.Interface.Repository;
using Inventarios.Entities.Models;
using Inventarios.Infrastructure.Context;

namespace Inventarios.Infrastructure.Repository;

public class InventarioStockRepository : BaseRepository<InventarioStock>, IInventarioStockRepository
{
    public InventarioStockRepository(InventariosDbContext context) : base(context)
    {
        
    }
    
}