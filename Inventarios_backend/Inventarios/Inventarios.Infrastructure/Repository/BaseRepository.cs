using Inventarios.Infrastructure.Context;
using Inventarios.Infrastructure.Interface;
using Microsoft.EntityFrameworkCore;

namespace Inventarios.Infrastructure.Repository;

public class BaseRepository<T> : IBaseRepository<T>, IDisposable
where T : class 
{
    private readonly InventariosDbContext _context;
    private readonly DbSet<T> _dbSet;
    
    public BaseRepository(InventariosDbContext context)
    {
        _context = context ??  throw new ArgumentNullException(nameof(context));
        _dbSet = _context.Set<T>(); // Inicializa clases dentro del DbContext
    }

    public async Task<IEnumerable<T>> ObtenerTodosAsync()
    {
        var items = await _dbSet.AsNoTracking().ToListAsync();
        return items;
    }

    public async Task<T?> ObtenerPorIdAsync(Guid id)
    {
        var item = await _dbSet.FindAsync(id);
        return item;
    }
    
    public async Task<T?> ObtenerPorIdAsync(int id)
    {
        var item = await _dbSet.FindAsync(id);
        return item;
    }

    public async Task<T> AgregarRegistroAsync(T entidad)
    {
        await _dbSet.AddAsync(entidad);
        await _context.SaveChangesAsync();
        return entidad;
    }

    public async Task<IEnumerable<T>> AgregarRegistrosAsync(IEnumerable<T> entidades)
    {
        var items = entidades.ToList();
        await _dbSet.AddRangeAsync(items);
        await _context.SaveChangesAsync();
        return items;
    }

    public async Task<bool> ActualizarRegistroAsync(Guid id, T entidad)
    {
        var item = await _dbSet.FindAsync(id);
        if (item != null)
        {
            _context.Update(item);
            await _context.SaveChangesAsync();
            return true;
        }
        return false;
    }
    
    public async Task<bool> ActualizarRegistroAsync(int id, T entidad)
    {
        var item = await _dbSet.FindAsync(id);
        if (item != null)
        {
            _context.Update(item);
            await _context.SaveChangesAsync();
            return true;
        }
        return false;
    }

    public async Task<bool> EliminarRegistroAsync(Guid id)
    {
        var item = await _dbSet.FindAsync(id);
        if (item is not null)
        {
            _dbSet.Remove(item);
            await _context.SaveChangesAsync();
            return true;
        }
        return false;
    }
    

    public async Task<bool> EliminarRegistroAsync(T entidad)
    {
        _context.Remove(entidad);
        await _context.SaveChangesAsync();
        return true;
    }

    public void Dispose()
    {
        _context.Dispose();
        GC.SuppressFinalize(this);
    }
}