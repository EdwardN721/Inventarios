using Inventarios.Entities.Models;
using Inventarios.Infrastructure.Context;
using Inventarios.Infrastructure.Interface;

namespace Inventarios.Infrastructure.Repository;

public class InventarioMovimientosRepository : BaseRepository<InventarioMovimientos>, IInventarioMovimientosRepository
{
    public InventarioMovimientosRepository(InventariosDbContext context) : base(context)
    {
        
    }
}