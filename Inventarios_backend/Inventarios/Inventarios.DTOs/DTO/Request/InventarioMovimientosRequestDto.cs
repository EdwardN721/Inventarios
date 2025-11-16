namespace Inventarios.DTOs.DTO.Request;

public record InventarioMovimientosRequestDto
{
    public DateTime FechaMovimiento { get; set; }
    public int Cantidad { get; set; }
};