using Inventarios.Bussiness.Interface.Repository;
using Inventarios.Entities.Models;
using Inventarios.Infrastructure.Context;

namespace Inventarios.Infrastructure.Repository;

public class InventarioMovimientosRepository : BaseRepository<InventarioMovimientos>, IInventarioMovimientosRepository
{
    public InventarioMovimientosRepository(InventariosDbContext context) : base(context)
    {
        
    }
}