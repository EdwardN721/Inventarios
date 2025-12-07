using Inventarios.Business.Exceptions;
using Inventarios.Business.Interface.Services;
using Inventarios.Business.Mappers;
using Inventarios.Bussiness.Interface.Repository;
using Inventarios.DTOs.DTO.Request;
using Inventarios.DTOs.DTO.Response;
using Inventarios.Entities.Models;
using Microsoft.Extensions.Logging;

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
        InventarioMovimiento movimiento = request.ToEntity();
        
        await _unitOfWork.InventarioMovimientosRepository.AgregarRegistro(movimiento);
        await _unitOfWork.SaveChangesAsync();
        
        _logger.LogInformation("Movimiento agregado con éxito");
        return movimiento.ToDto();
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
}