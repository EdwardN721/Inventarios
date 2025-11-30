using Inventarios.DTOs.DTO.Response;

namespace Inventarios.Bussiness.Interface.Services;

public interface IInventarioMovimientoService
{
    Task<IEnumerable<InventarioMovimientosResponseDto>> ObtenerMovimientos();
    Task<InventarioMovimientosResponseDto> ObtenerMovimientoPorId(Guid id);
    
}