namespace Inventarios.DTOs.DTO.Request;

public record InventarioStockRequestDto(
    int Cantidad,
    string Ubicacion,
    Guid ProductoId
    ){ };