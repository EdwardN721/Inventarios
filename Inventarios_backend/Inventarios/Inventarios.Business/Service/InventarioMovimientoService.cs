using Inventarios.Entities.Models;
using Microsoft.Extensions.Logging;
using Inventarios.Business.Mappers;
using Inventarios.DTOs.DTO.Request;
using Inventarios.DTOs.DTO.Response;
using Inventarios.Business.Exceptions;
using Inventarios.Business.Interface.Services;
using Inventarios.Business.Interface.Repository;
using Inventarios.Extensions;

namespace Inventarios.Business.Service;

public class InventarioMovimientoService : IInventarioMovimientoService
{
    private readonly ILogger<InventarioMovimientoService> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public InventarioMovimientoService(ILogger<InventarioMovimientoService> logger, IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<InventarioMovimientosResponseDto>> ObtenerMovimientos()
    {
        ICollection<InventarioMovimiento> movimientos = await _unitOfWork.InventarioMovimientosRepository.ObtenerTodosAsync();
        _logger.LogInformation("Movimientos obtenidos: {Count}", movimientos.Count);
        return movimientos.ToDto();
    }

    public async Task<InventarioMovimientosResponseDto> ObtenerMovimientoPorId(Guid id)
    {
        InventarioMovimiento? movimiento = await _unitOfWork.InventarioMovimientosRepository.ObtenerPorIdAsync(id);
        if (movimiento == null)
        {
            _logger.LogWarning("Movimiento no encontrado con el Id: {Id}", id);
            throw new NotFoundException($"Movimiento no encontrado con el Id: {id}");
        }

        return movimiento.ToDto();
    }

    public async Task<InventarioMovimientosResponseDto> CrearMovimiento(InventarioMovimientosRequestDto request)
    {
        await _unitOfWork.BeginTransactionAsync();
        try
        {
            TipoMovimiento tipoMovimiento =
                await _unitOfWork.TipoMovimientoRepository.ObtenerPorIdAsync(request.TipoMovimientoId)
                ?? throw new NotFoundException("El movimiento no éxiste.");

            InventarioStock? stockActual =
                await _unitOfWork.InventarioStockRepository.EnconcontrarPrimero(p =>
                    p.ProductoId == request.ProductoId);

            if (tipoMovimiento.EsSalida)
            {
                if (stockActual.Cantidad == 0 || stockActual.Cantidad < request.Cantidad)
                {
                    throw new BusinessExcepion($"Stock insuficiente. Disponible: {stockActual?.Cantidad ?? 0}.");
                }

                stockActual.Cantidad -= request.Cantidad;
                stockActual.UpdatedAt = DateTime.UtcNow;
                _unitOfWork.InventarioStockRepository.ActualizarRegistro(stockActual);
            }
            else if (tipoMovimiento.EsEntrada)
            {
                if (stockActual == null)
                {
                    stockActual = new InventarioStock
                    {
                        ProductoId = request.ProductoId,
                        Cantidad = request.Cantidad,
                        Ubicacion = "ALMACEN GENERAL",
                        CreatedAt = DateTime.UtcNow,
                    };
                    await _unitOfWork.InventarioStockRepository.AgregarRegistro(stockActual);
                }
                else
                {
                    stockActual.Cantidad += request.Cantidad;
                    stockActual.UpdatedAt = DateTime.UtcNow;
                    _unitOfWork.InventarioStockRepository.ActualizarRegistro(stockActual);
                }
            }
            InventarioMovimiento nuevoMovimiento = request.ToEntity();
            nuevoMovimiento.FechaMovimiento = DateTime.UtcNow;

            await _unitOfWork.InventarioMovimientosRepository.AgregarRegistro(nuevoMovimiento);

            await _unitOfWork.SaveChangesAsync();
            await _unitOfWork.CommitTransactionAsync();

            _logger.LogInformation("Movimiento {Id} agregado con exito. Nuevo stock: {StockCantidad}",
                nuevoMovimiento.ProductoId, stockActual.Cantidad);
            return nuevoMovimiento.ToDto();
        }
        catch (Exception)
        {
            await _unitOfWork.RollbackTransactionAsync();
            throw;
        }
    }

    public async Task ActualizarMovimiento(Guid id, InventarioMovimientosRequestDto request)
    {
        InventarioMovimiento? movimiento = await _unitOfWork.InventarioMovimientosRepository.ObtenerPorIdAsync(id);
        if (movimiento == null)
        {
            _logger.LogWarning("Movimiento no encontrado con el Id: {Id}", id);
            throw new NotFoundException($"Movimiento no encontrado con el Id: {id}");
        }
        
        request.ToUpdateEntity(movimiento);
        
        await _unitOfWork.SaveChangesAsync();
        _logger.LogInformation("Movimiento actualizado con exito");
    }

    public async Task EliminarMovimiento(Guid id)
    {
        InventarioMovimiento? movimiento = await _unitOfWork.InventarioMovimientosRepository.ObtenerPorIdAsync(id);
        if (movimiento == null)
        {
            _logger.LogWarning("Movimiento no encontrado con el Id: {Id}", id);
            throw new NotFoundException($"Movimiento no encontrado con el Id: {id}");
        }
        
        _unitOfWork.InventarioMovimientosRepository.EliminarRegistro(movimiento);
        await _unitOfWork.SaveChangesAsync();
        _logger.LogInformation("Movimiento eliminado con exito");
    }

    public async Task<int> StockDisponiblePorProducto(Guid productoId)
    {
        InventarioStock inventario =  await _unitOfWork.InventarioStockRepository.EnconcontrarPrimero(i => i.ProductoId == productoId) 
                                      ?? throw new NotFoundException($"Producto no encontrado con el Id: {productoId}");
        
        return inventario.Cantidad;
    }

    #region MetodosPrivados

    private InventarioMovimiento ComprarAProveedor(InventarioMovimiento movimiento)
    {
        movimiento.Cantidad += movimiento.Cantidad;
        return movimiento;
    }

    private async Task<InventarioMovimiento> VentaDirecta(InventarioMovimiento movimiento)
    {
        int stockDisponible = await StockDisponiblePorProducto(movimiento.ProductoId);

        if (stockDisponible == 0)
        {
            throw new BusinessExcepion("Inventario vacío.");
        }
        else if (stockDisponible < movimiento.Cantidad)
        {
            throw new BusinessExcepion("El stock actual no cubre con la cantidad solicitada.");
        }

        movimiento.Cantidad -= movimiento.Cantidad;
        return movimiento; 
    }

    #endregion
}