using Inventarios.Entities.Models;
using Inventarios.Infrastructure.Context;
using Inventarios.Infrastructure.Interface;

namespace Inventarios.Infrastructure.Repository;

public class InventarioStockRepository : BaseRepository<InventarioStock>, IInventatioStockRepository
{
    public InventarioStockRepository(InventariosDbContext context) : base(context)
    {
        
    }
    
}