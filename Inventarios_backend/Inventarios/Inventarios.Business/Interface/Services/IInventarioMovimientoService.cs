using Inventarios.DTOs.DTO.Request;
using Inventarios.DTOs.DTO.Response;

namespace Inventarios.Business.Interface.Services;

public interface IInventarioMovimientoService
{
    Task<IEnumerable<InventarioMovimientosResponseDto>> ObtenerMovimientos();
    Task<InventarioMovimientosResponseDto> ObtenerMovimientoPorId(Guid id);
    Task<InventarioMovimientosResponseDto> CrearMovimiento(InventarioMovimientosRequestDto request);
    Task ActualizarMovimiento(Guid id, InventarioMovimientosRequestDto request);
    Task EliminarMovimiento(Guid id);
}