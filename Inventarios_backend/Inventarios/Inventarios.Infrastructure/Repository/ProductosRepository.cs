using Inventarios.Bussiness.Interface.Repository;
using Inventarios.Entities.Models;
using Inventarios.Infrastructure.Context;

namespace Inventarios.Infrastructure.Repository;

public class ProductosRepository : BaseRepository<Productos>, IProductosRepository
{
    public ProductosRepository(InventariosDbContext context ) : base(context)
    {
        
    }
}