namespace Inventarios.DTOs.DTO.Request;

public record InventarioStockRequestDto
{
    public int Cantidad { get; set; }
    public string Ubicacion { get; set; } = string.Empty;
};