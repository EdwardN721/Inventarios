using Inventarios.DTOs.DTO.Request;

namespace Inventarios.DTOs.DTO.Response;

public record InventarioStockResponseDto : InventarioStockRequestDto
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}