namespace Inventarios.Infrastructure.Interface;

public interface IBaseRepository<T>
where T : class // Donde T es una clase
{
    /// <summary>
    /// Obtener entidad por ID (string, int, Guid) 
    /// </summary>
    Task<T?> ObtenerPorIdAsync(object id);
    
    /// <summary>
    /// Obtener todas las entidades
    /// </summary>
    /// <returns></returns>
    Task<IEnumerable<T>> ObtenerTodosAsync();
    
    /// <summary>
    /// Permite consultas como .Where(x => x.Activo == true)
    /// </summary>
    IQueryable<T> AsQueryable();
    
    /// <summary>
    /// Prepara una nueva entidad para ser inserada
    /// </summary>
    Task AgregarRegistro(T entidad);
    
    /// <summary>
    /// Prepara una lista de entidades a insertar
    /// </summary>
    Task AgregarRegistrosAsync(IEnumerable<T> registros);
    
    /// <summary>
    /// Marca una entidad como actualizada
    /// </summary>
    void ActualizarRegistro(T entidad);
    
    /// <summary>
    /// Marca una entidad para ser eliminada
    /// </summary>
    void EliminarRegistro(T entidad);
    
    
    /*¨Task<IEnumerable<T>> ObtenerTodosAsync();
    Task<T?> ObtenerPorIdAsync(Guid id);
    Task<T?> ObtenerPorIdAsync(int id);
    Task<T> AgregarRegistroAsync(T entidad);
    Task<IEnumerable<T>> AgregarRegistrosAsync(IEnumerable<T> entidades);
    Task<bool> ActualizarRegistroAsync(Guid id, T entidad);
    Task<bool> ActualizarRegistroAsync(int id, T entidad);
    Task<bool> EliminarRegistroAsync(Guid id);
    Task<bool> EliminarRegistroAsync(T entidad);
    Task<bool> ActualizarRegistroAsync(T entidad);*/
    
}