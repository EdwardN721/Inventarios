namespace Inventarios.Entities.Models;

public class InventarioStock
{
    public Guid Id { get; set; }
    public Guid ProductoId { get; set; }
    public int Cantidad { get; set; } 
    public string Ubicacion { get; set; } = string.Empty;
    public DateTime Creado { get; set; }
    public DateTime Modificado { get; set; }
}