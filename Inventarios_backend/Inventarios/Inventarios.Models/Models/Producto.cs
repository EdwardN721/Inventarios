using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Inventarios.Entities.Models;

public class Producto
{
    public Guid Id { get; set; }
    [MaxLength(100)]
    public required string Nombre { get; set; } = string.Empty;
    [MaxLength(250)]
    public string? Descripcion { get; set; } = string.Empty;
    [Column(TypeName = "decimal(18,2)")]
    public decimal Precio { get; set; }
    [MaxLength(50)]
    public string CodigoBarras { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    
    // Relación uno a uno
    public int CategoriaId { get; set; }
    public virtual Categoria? Categoria { get; set; } // <- Propiedad de navegación
 
    // Relación uno a uno
    public Guid ProveedorId { get; set; }
    public virtual Proveedor? Proveedor { get; set; } // <- Propiedad de navegación
    
    // Relación uno a muchos
    public virtual ICollection<InventarioMovimiento>?  InventarioMovimientos { get; set; } = new List<InventarioMovimiento>();
    
    // Relacioón uno a muchos
    public virtual ICollection<InventarioStock>?  InventarioStock { get; set; }  = new List<InventarioStock>();
}