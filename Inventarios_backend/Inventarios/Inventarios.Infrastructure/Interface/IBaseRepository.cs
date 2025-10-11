namespace Inventarios.Infrastructure.Interface;

public interface IBaseRepository<T>
where T : class // Donde T es una clase
{
    Task<IEnumerable<T>> OBtenerTodosAsync();
    Task<T?> ObtenerPorIdAsync(Guid id);
    Task<T> CrearRegistroAsync(T entidad);
    Task<IEnumerable<T>> AgregarRegistrosAsync(IEnumerable<T> entidades);
    Task<bool> ActualizarRegistroAsync(Guid id, T entidad);
    Task<bool> EliminarRegistroAsync(Guid id);
    Task<bool> EliminarRegistroAsync(T entidad);
}