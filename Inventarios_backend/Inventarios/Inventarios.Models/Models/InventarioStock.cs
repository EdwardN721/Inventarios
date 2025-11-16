namespace Inventarios.Entities.Models;

public class InventarioStock
{
    public Guid Id { get; set; }
    public int Cantidad { get; set; } 
    public string Ubicacion { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    
    // Relación uno a uno
    public Guid ProductoId { get; set; }
    public Productos? Producto { get; set; } // <- Propiedades de navegación
}