namespace Inventarios.DTOs.DTO.Response;

public record InventarioStockResponseDto(
    Guid Id,
    int Cantidad,
    string Ubicacion,
    DateTime CreatedAt,
    DateTime? UpdateAt,
    Guid ProductoId
    ) { };