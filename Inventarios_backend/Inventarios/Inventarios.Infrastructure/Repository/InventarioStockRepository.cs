using Inventarios.Entities.Models;
using Inventarios.Infrastructure.Context;
using Inventarios.Business.Interface.Repository;

namespace Inventarios.Infrastructure.Repository;

public class InventarioStockRepository : BaseRepository<InventarioStock>, IInventarioStockRepository
{
    public InventarioStockRepository(InventariosDbContext context) : base(context)
    {
        
    }
    
}