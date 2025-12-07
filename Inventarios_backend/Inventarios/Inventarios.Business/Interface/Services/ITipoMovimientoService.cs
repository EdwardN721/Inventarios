using Inventarios.DTOs.DTO.Request;
using Inventarios.DTOs.DTO.Response;

namespace Inventarios.Business.Interface.Services;

public interface ITipoMovimientoService
{
    Task<IEnumerable<TipoMovimientoResponseDto>> ObtenerTodosTiposMovimientos();
    Task<TipoMovimientoResponseDto> ObtenerTipoMovimientoPorId(int id);
    Task<TipoMovimientoResponseDto> AgregarTipoMovimiento(TipoMovimientoRequestDto tipoMovimientoDto);
    Task<bool> ActualizarTipoMovimiento(int id, TipoMovimientoRequestDto tipoMovimientoDto);
    Task<bool> EliminarTipoMovimiento(int id);
}