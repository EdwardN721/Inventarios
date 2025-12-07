using Inventarios.DTOs.DTO.Request;

namespace Inventarios.DTOs.DTO.Response;

public record InventarioMovimientosResponseDto(
    Guid Id,
    DateTime FechaMovimiento,
    int Cantiad,
    int TipoMovimiento,
    Guid ProductoId
);
