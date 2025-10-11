using Inventarios.Entities.Models;
using Inventarios.Infrastructure.Context;
using Inventarios.Infrastructure.Interface;

namespace Inventarios.Infrastructure.Repository;

public class TipoMovimientoRepository : BaseRepository<TipoMovimiento>, ITipoMovimientoRepository
{
    public TipoMovimientoRepository(InventariosDbContext context) : base(context)
    {
        
    }
}