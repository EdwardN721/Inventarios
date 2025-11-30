using Inventarios.Bussiness.Exceptions;
using Inventarios.Bussiness.Interface.Repository;
using Inventarios.Bussiness.Interface.Services;
using Inventarios.Bussiness.Mappers;
using Inventarios.DTOs.DTO.Request;
using Inventarios.DTOs.DTO.Response;
using Inventarios.Entities.Models;
using Microsoft.Extensions.Logging;

namespace Inventarios.Bussiness.Service;

public class InventarioStockService : IInventarioStockService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<InventarioStockService> _logger;

    public InventarioStockService(IUnitOfWork unitOfWork, ILogger<InventarioStockService> logger)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _logger = logger;
    }


    public async Task<IEnumerable<InventarioStockResponseDto>> ObtenerInventariosStocks()
    {
        _logger.LogInformation("ObtenerInventariosStocks");
        IEnumerable<InventarioStock> inventarios =
            await _unitOfWork.InventarioStockRepository.ObtenerTodosAsync();
        _logger.LogInformation("ObtenerInventariosStocks finished.");
        return ManualMapperInventarioStock.ToInventarioStockResponse(inventarios);
    }

    public async Task<InventarioStockResponseDto> ObtenerInventarioStockPorId(Guid id)
    {
        _logger.LogInformation("ObtenerInventarioStockPorId {Id}", id);
        InventarioStock? inventarioStock = await _unitOfWork.InventarioStockRepository.ObtenerPorIdAsync(id);

        if (inventarioStock == null)
        {
            _logger.LogInformation("No existe Inventario Stock {Id}", id);
            throw new NotFoundException("No existe Inventario Stock");
        }
        
        _logger.LogInformation("Stock encontrado: {Id}", id);
        return ManualMapperInventarioStock.ToInventarioStockResponse(inventarioStock);
    }

    public async Task<InventarioStockResponseDto> InsertarInventarioStock(InventarioStockRequestDto inventarioStockRequestDto)
    {
        _logger.LogInformation("Creando Inventario Stock");
        InventarioStock stock = ManualMapperInventarioStock.ToInventarioStock(inventarioStockRequestDto);
        
        await _unitOfWork.InventarioStockRepository.AgregarRegistro(stock);
        await _unitOfWork.SaveChangesAsync();
        
        _logger.LogInformation("Inventario Stock agregado");
        return ManualMapperInventarioStock.ToInventarioStockResponse(stock);
    }

    public async Task ActualizarInventarioStock(Guid id, InventarioStockRequestDto inventarioStockRequestDto)
    {
        _logger.LogInformation("Actualizando Inventario Stock {Id}", id);
        InventarioStock? inventario = await _unitOfWork.InventarioStockRepository.ObtenerPorIdAsync(id);
        if (inventario == null)
        {
            _logger.LogWarning("No existe Inventario Stock {Id}", id);
            throw new NotFoundException("No existe ese Stock del Inventario.");
        }
        
        ManualMapperInventarioStock.ToUpdateInventarioStock(inventario, inventarioStockRequestDto);
        inventario.UpdatedAt = DateTime.UtcNow;
        await _unitOfWork.SaveChangesAsync();
        
        _logger.LogInformation("Inventario Stock actualizado");
    }

    public async Task EliminarInventarioStock(Guid id)
    {
        _logger.LogInformation("Eliminando Stock {Id}", id);
        InventarioStock? inventario = await _unitOfWork.InventarioStockRepository.ObtenerPorIdAsync(id);
        if (inventario == null)
        {
            _logger.LogWarning("No existe Inventario Stock {Id}", id);
            throw new NotFoundException("No existe ese Stock del Inventario.");
        }
        
        _unitOfWork.InventarioStockRepository.EliminarRegistro(inventario);
        await _unitOfWork.SaveChangesAsync();
        
        _logger.LogInformation("Inventario Stock eliminado.");
    }
}