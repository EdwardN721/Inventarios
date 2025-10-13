namespace Inventarios.Entities.Models;

public class Productos
{
    public Guid Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string? Descripcion { get; set; } = string.Empty;
    public decimal Precio { get; set; }
    public string CodigoBarras { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    
    // Relación uno a uno
    public int IdCategoria { get; set; }
    public Categorias? Categoria { get; set; } // <- Propiedad de navegación
 
    // Relación uno a uno
    public Guid IdProveedor { get; set; }
    public Proveedor? Proveedor { get; set; } // <- Propiedad de navegación
    
    // Relación uno a muchos
    public IEnumerable<InventarioMovimientos>?  InventarioMovimientos { get; set; }
    
    // Relacioón uno a muchos
    public IEnumerable<InventarioStock>?  InventarioStock { get; set; }
}