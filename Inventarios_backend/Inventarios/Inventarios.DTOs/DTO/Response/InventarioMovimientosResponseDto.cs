using Inventarios.DTOs.DTO.Request;

namespace Inventarios.DTOs.DTO.Response;

public record InventarioMovimientosResponseDto{
    public Guid Id { get; init; }
    public DateTime FechaMovimiento { get; init; }
    public int Cantidad { get; init; }
};