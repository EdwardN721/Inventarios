namespace Inventarios.Entities.Models;

public class InventarioMovimientos
{
    public Guid Id { get; set; }
    public DateTime FechaMovimiento { get; set; }
    public int Cantidad { get; set; }

    // Relación uno a uno
    public int IdTipoMovimiento  { get; set; }
    public TipoMovimiento? TipoMovimiento { get; set; } // <- Propiedad de navegación
    
    // Relación uno a uno
    public Guid ProductoId { get; set; }
    public Productos? Producto { get; set; } // <- Propiedades de navegación
}