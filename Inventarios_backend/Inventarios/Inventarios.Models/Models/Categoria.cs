using System.ComponentModel.DataAnnotations;

namespace Inventarios.Entities.Models;

public class Categoria
{
    public int Id { get; set; }
    [MaxLength(50)]
    public required string Nombre { get; set; } = string.Empty;
    [MaxLength(250)]
    public string? Descripcion { get; set; } = string.Empty;
    public required DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    
    // Relación Uno-a-Muchos: Una categoría tiene MUCHOS productos
    public virtual ICollection<Producto>? Productos { get; set; } = new List<Producto>();
} 