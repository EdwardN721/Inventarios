using Inventarios.Entities.Models;
using Inventarios.Infrastructure.Context;
using Inventarios.Infrastructure.Interface;

namespace Inventarios.Infrastructure.Repository;

public class CategoriasRepository : BaseRepository<Categorias>, ICategoriasRepository
{
    public CategoriasRepository(InventariosDbContext context) : base(context)
    {
        
    }
}