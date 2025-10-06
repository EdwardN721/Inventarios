namespace Inventarios.Entities.Models;

public class Categorias
{
    public Guid Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
    public DateTime Creado { get; set; } 
    public DateTime Modificado { get; set; }
    
    // Relación uno a muchos
    public IEnumerable<Productos>? Productos { get; set; }
} 