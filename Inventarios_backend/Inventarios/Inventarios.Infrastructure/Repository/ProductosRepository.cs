using Inventarios.Bussiness.Interface.Repository;
using Inventarios.Entities.Models;
using Inventarios.Infrastructure.Context;

namespace Inventarios.Infrastructure.Repository;

public class ProductosRepository : BaseRepository<Producto>, IProductosRepository
{
    public ProductosRepository(InventariosDbContext context ) : base(context)
    {
        
    }
}