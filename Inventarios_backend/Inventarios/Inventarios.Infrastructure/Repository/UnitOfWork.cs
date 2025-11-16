using Inventarios.Bussiness.Interface.Repository;
using Inventarios.Infrastructure.Context;
using Microsoft.EntityFrameworkCore.Storage;

namespace Inventarios.Infrastructure.Repository;

public class UnitOfWork : IUnitOfWork
{
    private readonly InventariosDbContext _context;
    private IDbContextTransaction? _transaction;
    
    private ICategoriasRepository? _categoriasRepository;
    private IInventarioMovimientosRepository? _inventarioMovimientosRepository;
    private IInventarioStockRepository? _inventarioStockRepository;
    private IProductosRepository? _productosRepository;
    private IProveedorRepository? _proveedorRepository;
    private ITipoMovimientoRepository? _tipoMovimientoRepository;

    public UnitOfWork(InventariosDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }
    
    public ICategoriasRepository CategoriasRepository =>
        _categoriasRepository ??= new CategoriasRepository(_context);
    
    public IInventarioMovimientosRepository InventarioMovimientosRepository =>
        _inventarioMovimientosRepository ??= new InventarioMovimientosRepository(_context);
    
    public IInventarioStockRepository InventarioStockRepository =>
        _inventarioStockRepository ??= new InventarioStockRepository(_context);
    
    public IProductosRepository ProductosRepository =>
        _productosRepository ??= new ProductosRepository(_context);
    
    public IProveedorRepository ProveedorRepository => 
        _proveedorRepository ??= new ProveedorRepository(_context);
    
    public ITipoMovimientoRepository TipoMovimientoRepository => 
        _tipoMovimientoRepository ??= new TipoMovimientoRepository(_context);

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public async Task BeginTransactionAsync()
    {
        _transaction = await _context.Database.BeginTransactionAsync();
    }

    public async Task CommitTransactionAsync()
    {
        if (_transaction == null)
        {
            return;
        }
        
        try
        {
            await _transaction.CommitAsync();
        }
        finally
        {
            await _transaction.DisposeAsync();
            _transaction = null;
        }
    }

    public async Task RollbackTransactionAsync()
    {
        if (_transaction == null)
        {
            return;
        }
        
        try
        {
            await _transaction.RollbackAsync();
        }
        finally
        {
            await _transaction.DisposeAsync();
            _transaction = null;
        }
    }

    public void Dispose()
    {
        if (_transaction != null)
        {
            _transaction.Dispose();
        }
        _context.Dispose();
        GC.SuppressFinalize(this);
    }
}