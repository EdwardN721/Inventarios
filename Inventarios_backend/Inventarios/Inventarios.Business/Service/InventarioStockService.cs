using Inventarios.Business.Exceptions;
using Inventarios.Business.Interface.Services;
using Inventarios.Business.Mappers;
using Inventarios.Bussiness.Interface.Repository;
using Inventarios.DTOs.DTO.Request;
using Inventarios.DTOs.DTO.Response;
using Inventarios.Entities.Models;
using Microsoft.Extensions.Logging;

namespace Inventarios.Business.Service;

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
        return inventarios.ToDto();
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
        return inventarioStock.ToDto();
    }

    public async Task<InventarioStockResponseDto> InsertarInventarioStock(InventarioStockRequestDto inventarioStockRequestDto)
    {
        _logger.LogInformation("Creando Inventario Stock");
        // InventarioStock stock = ManualMapperInventarioStock.ToInventarioStock(inventarioStockRequestDto);
        InventarioStock stock = inventarioStockRequestDto.ToEntity();
        
        await _unitOfWork.InventarioStockRepository.AgregarRegistro(stock);
        await _unitOfWork.SaveChangesAsync();
        
        _logger.LogInformation("Inventario Stock agregado");
        return stock.ToDto();
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
        
        inventarioStockRequestDto.ToUpdateEntity(inventario);
        
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