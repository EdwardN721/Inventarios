namespace Inventarios.Entities.Models;

public class Categorias
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string? Descripcion { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    
    // Relación uno a muchos
    public IEnumerable<Productos>? Productos { get; set; }
} 