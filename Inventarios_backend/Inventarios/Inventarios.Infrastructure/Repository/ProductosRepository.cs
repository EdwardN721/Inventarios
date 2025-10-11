using Inventarios.Entities.Models;
using Inventarios.Infrastructure.Context;
using Inventarios.Infrastructure.Interface;

namespace Inventarios.Infrastructure.Repository;

public class ProductosRepository : BaseRepository<Productos>, IProductosRepository
{
    public ProductosRepository(InventariosDbContext context ) : base(context)
    {
        
    }
}