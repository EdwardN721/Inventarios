namespace Inventarios.Entities.Models;

public class TipoMovimiento
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string? Descripcion { get; set; } = string.Empty;
    public bool EsEntrada { get; set; } =  false;
    public bool EsSalida { get; set; }  = false;
    public bool EsTransferenciaInterna { get; set; }  = false;
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    
    //Relación uno a muchos
    public IEnumerable<InventarioMovimientos>?  InventarioMovimientos { get; set; } 
}