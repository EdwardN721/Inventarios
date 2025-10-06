namespace Inventarios.Entities.Models;

public class TipoMovimiento
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
    public bool EsEntrada { get; set; }
    public bool EsSalida { get; set; }
    public bool EsTransferenciaInterna { get; set; }
    public DateTime Creado { get; set; }
    public DateTime Modificado { get; set; }
    
    //Relación uno a muchos
    public IEnumerable<InventarioMovimientos>?  InventarioMovimientos { get; set; } 
}