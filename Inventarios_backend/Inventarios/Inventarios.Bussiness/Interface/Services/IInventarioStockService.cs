using Inventarios.DTOs.DTO.Request;
using Inventarios.DTOs.DTO.Response;
using Inventarios.Entities.Models;

namespace Inventarios.Bussiness.Interface.Services;

public interface IInventarioStockService
{
    Task<IEnumerable<InventarioStockResponseDto>> ObtenerInventariosStocks();
    Task<InventarioStockResponseDto> ObtenerInventarioStockPorId(Guid id);
    Task<InventarioStockResponseDto> InsertarInventarioStock(InventarioStockRequestDto inventarioStockRequestDto);
    Task ActualizarInventarioStock(Guid id, InventarioStockRequestDto inventarioStockRequestDto);
    Task EliminarInventarioStock(Guid id);
}