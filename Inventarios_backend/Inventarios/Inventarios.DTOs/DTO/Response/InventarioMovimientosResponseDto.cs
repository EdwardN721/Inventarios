using Inventarios.DTOs.DTO.Request;

namespace Inventarios.DTOs.DTO.Response;

public record InventarioMovimientosResponseDto : InventarioMovimientosRequestDto
{
    public Guid Id { get; set; }
};