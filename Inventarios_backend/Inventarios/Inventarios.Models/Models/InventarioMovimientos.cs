namespace Inventarios.Entities.Models;

public class InventarioMovimientos
{
    public Guid Id { get; set; }
    public Guid ProductoId { get; set; }
    public DateTime FechaMovimiento { get; set; }
    public int IdTipoMovimiento  { get; set; }
    public int Cantidad { get; set; }

}