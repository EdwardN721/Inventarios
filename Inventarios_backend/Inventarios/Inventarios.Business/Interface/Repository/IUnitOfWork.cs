namespace Inventarios.Business.Interface.Repository;

public interface IUnitOfWork : IDisposable
{
    ICategoriasRepository CategoriasRepository { get; }
    IInventarioMovimientosRepository InventarioMovimientosRepository { get; }
    IInventarioStockRepository InventarioStockRepository { get; }
    IProductosRepository ProductosRepository { get; }
    IProveedorRepository ProveedorRepository { get; }
    ITipoMovimientoRepository TipoMovimientoRepository { get; }
    
    /// <summary>
    /// Guarda todos los cambios pendientes en una transaccion
    /// Si falla,  hace rollback automaticamente 
    /// </summary>
    Task<int> SaveChangesAsync();
    
    /// <summary>
    /// Inicia una nueva transaccion manual en la base de datos
    /// </summary>
    /// <returns></returns>
    Task BeginTransactionAsync();
    
    /// <summary>
    /// Conrfima (Commit) la transaccion activa
    /// </summary>
    Task CommitTransactionAsync();
    
    /// <summary>
    /// Revierte la transaccion activa
    /// </summary>
    Task RollbackTransactionAsync();
}