using Inventarios.Entities.Models;
using Inventarios.Infrastructure.Context;
using Inventarios.Business.Interface.Repository;

namespace Inventarios.Infrastructure.Repository;

public class ProductosRepository : BaseRepository<Producto>, IProductosRepository
{
    public ProductosRepository(InventariosDbContext context ) : base(context)
    {
        
    }
}