namespace Inventarios.Entities.Models;

public class Proveedor
{
    public Guid Id { get; set; }
    public string Nombre { get; set; } = string.Empty; 
    public string PersonaContacto { get; set; } = string.Empty;
    public string Telefono { get; set; } = string.Empty;
    public string Correo  { get; set; } = string.Empty;
    public string Direccion { get; set; } = string.Empty;
    public DateTime Creado { get; set; }
    public DateTime Modificado { get; set; }
    
    // Relación uno a muchos
    public IEnumerable<Productos>? Productos { get; set; }
}