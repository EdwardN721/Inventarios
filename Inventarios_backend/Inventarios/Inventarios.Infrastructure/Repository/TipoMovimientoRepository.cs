using Inventarios.Bussiness.Interface.Repository;
using Inventarios.Entities.Models;
using Inventarios.Infrastructure.Context;

namespace Inventarios.Infrastructure.Repository;

public class TipoMovimientoRepository : BaseRepository<TipoMovimiento>, ITipoMovimientoRepository
{
    public TipoMovimientoRepository(InventariosDbContext context) : base(context)
    {
        
    }
}