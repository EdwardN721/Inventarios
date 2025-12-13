using Inventarios.Entities.Models;
using Inventarios.Infrastructure.Context;
using Inventarios.Business.Interface.Repository;

namespace Inventarios.Infrastructure.Repository;

public class InventarioMovimientosRepository : BaseRepository<InventarioMovimiento>, IInventarioMovimientosRepository
{
    public InventarioMovimientosRepository(InventariosDbContext context) : base(context)
    {
        
    }
}