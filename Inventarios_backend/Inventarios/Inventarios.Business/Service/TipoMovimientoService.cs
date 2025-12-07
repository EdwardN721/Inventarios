using Inventarios.Business.Exceptions;
using Inventarios.Business.Interface.Services;
using Inventarios.Business.Mappers;
using Inventarios.Bussiness.Interface.Repository;
using Inventarios.DTOs.DTO.Request;
using Inventarios.DTOs.DTO.Response;
using Inventarios.Entities.Models;
using Microsoft.Extensions.Logging;

namespace Inventarios.Business.Service;

public class TipoMovimientoService : ITipoMovimientoService
{
    private readonly ILogger<TipoMovimientoService> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public TipoMovimientoService(ILogger<TipoMovimientoService> logger, IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<TipoMovimientoResponseDto>> ObtenerTodosTiposMovimientos()
    {
        IEnumerable<TipoMovimiento> tipoMovimientos = await _unitOfWork.TipoMovimientoRepository.ObtenerTodosAsync();
        
        _logger.LogInformation("Tipos movimientos obtenidos {Count}", tipoMovimientos.Count());
        return tipoMovimientos.ToDto();
    }

    public async Task<TipoMovimientoResponseDto> ObtenerTipoMovimientoPorId(int id)
    {
        TipoMovimiento tipoMovimiento = await _unitOfWork.TipoMovimientoRepository.ObtenerPorIdAsync(id) 
                                       ?? throw new NotFoundException($"No se encontro el tipo movimiento Id: {id}");
        return tipoMovimiento.ToDto();
    }

    public async Task<TipoMovimientoResponseDto> AgregarTipoMovimiento(TipoMovimientoRequestDto tipoMovimientoDto)
    {
        TipoMovimiento tipoMovimiento = tipoMovimientoDto.ToEntity();
        await _unitOfWork.TipoMovimientoRepository.AgregarRegistro(tipoMovimiento);
        await _unitOfWork.SaveChangesAsync();
        
        _logger.LogInformation("Movimiento agregado {}", tipoMovimiento.Nombre);
        return tipoMovimiento.ToDto();
    }

    public async Task<bool> ActualizarTipoMovimiento(int id, TipoMovimientoRequestDto tipoMovimientoDto)
    {
        TipoMovimiento tipoMovimiento = await _unitOfWork.TipoMovimientoRepository.ObtenerPorIdAsync(id) 
                                        ?? throw new NotFoundException($"No se encontro el tipo movimiento Id: {id}");
        
        tipoMovimientoDto.ToUpdateEntity(tipoMovimiento);
        
        _unitOfWork.TipoMovimientoRepository.ActualizarRegistro(tipoMovimiento);
        int actualizado = await _unitOfWork.SaveChangesAsync();
        
        _logger.LogInformation("Movimiento actualizado {Id}", id);
        return actualizado > 0;
    }

    public async Task<bool> EliminarTipoMovimiento(int id)
    {
        TipoMovimiento tipoMovimiento = await _unitOfWork.TipoMovimientoRepository.ObtenerPorIdAsync(id) 
                                        ?? throw new NotFoundException($"No se encontro el tipo movimiento Id: {id}");
        
        _unitOfWork.TipoMovimientoRepository.EliminarRegistro(tipoMovimiento);
        int eliminado = await _unitOfWork.SaveChangesAsync();
        
        _logger.LogInformation("Movimiento eliminado {}", id);
        return eliminado > 0;
    }
}