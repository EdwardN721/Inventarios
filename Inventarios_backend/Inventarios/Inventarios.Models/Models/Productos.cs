namespace Inventarios.Entities.Models;

public class Productos
{
    public Guid Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
    public decimal Precio { get; set; }
    public string CodigoBarras { get; set; } = string.Empty;
    public DateTime Creado { get; set; }
    public DateTime Modificado { get; set; }
}