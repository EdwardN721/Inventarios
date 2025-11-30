using Inventarios.Bussiness.Interface.Repository;
using Inventarios.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Inventarios.Infrastructure.Repository;

public class BaseRepository<T> : IBaseRepository<T>
where T : class 
{
    private readonly InventariosDbContext _context;
    private readonly DbSet<T> _dbSet;
    
    public BaseRepository(InventariosDbContext context)
    {
        _context = context ??  throw new ArgumentNullException(nameof(context));
        _dbSet = _context.Set<T>(); // Inicializa clases dentro del DbContext
    }

    public async Task<T?> ObtenerPorIdAsync(object id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task<IEnumerable<T>> ObtenerTodosAsync()
    {
        return await _dbSet.AsNoTracking().ToListAsync();
    }

    public IQueryable<T> AsQueryable()
    {
        return _dbSet.AsQueryable();
    }

    public async Task AgregarRegistro(T entidad)
    {
        await _dbSet.AddAsync(entidad);
    }

    public async Task AgregarRegistrosAsync(IEnumerable<T> registros)
    {
        await _dbSet.AddRangeAsync(registros);
    }

    public void ActualizarRegistro(T entidad)
    {
        _dbSet.Update(entidad);
    }

    public void EliminarRegistro(T entidad)
    {
        _dbSet.Remove(entidad);
    }
}