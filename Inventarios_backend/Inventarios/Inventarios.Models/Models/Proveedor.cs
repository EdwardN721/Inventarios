using System.ComponentModel.DataAnnotations;

namespace Inventarios.Entities.Models;

public class Proveedor
{
    public Guid Id { get; set; }
    [MaxLength(100)]
    public required string Nombre { get; set; } = string.Empty; 
    public string PersonaContacto { get; set; } = string.Empty;
    public string Telefono { get; set; } = string.Empty;
    public string Correo  { get; set; } = string.Empty;
    public string Direccion { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    
    // Relación uno a muchos
    public virtual ICollection<Producto>? Productos { get; set; } = new List<Producto>();
}