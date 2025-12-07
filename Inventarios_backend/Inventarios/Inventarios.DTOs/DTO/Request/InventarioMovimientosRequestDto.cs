namespace Inventarios.DTOs.DTO.Request;

public record InventarioMovimientosRequestDto(
    DateTime FechaMovimiento,
    int Cantidad,
    int TipoMovimientoId,
    Guid ProductoId
    );