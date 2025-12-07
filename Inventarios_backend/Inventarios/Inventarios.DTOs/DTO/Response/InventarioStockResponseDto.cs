using Inventarios.DTOs.DTO.Request;

namespace Inventarios.DTOs.DTO.Response;

public record InventarioStockResponseDto
{
    public Guid Id { get; init; }
    public int Cantidad { get; init; } 
    public string Ubicacion { get; init; } = string.Empty;
    public DateTime CreatedAt { get; init; }
    public DateTime? UpdatedAt { get; init; }
}