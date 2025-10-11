using Inventarios.Entities.Models;
using Inventarios.Infrastructure.Context;
using Inventarios.Infrastructure.Interface;

namespace Inventarios.Infrastructure.Repository;

public class ProveedorRepository : BaseRepository<Proveedor>, IProveedorRepository
{
    public ProveedorRepository(InventariosDbContext context) : base(context)
    {
        
    }
}