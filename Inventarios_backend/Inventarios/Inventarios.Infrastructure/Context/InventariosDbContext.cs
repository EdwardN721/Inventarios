using Inventarios.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Inventarios.Infrastructure.Context;

public class InventariosDbContext : DbContext
{
    public InventariosDbContext(DbContextOptions<InventariosDbContext> options)  : base(options)
    {
        
    }
    
   public DbSet<Productos> Productos { get; set; }
   public DbSet<Categorias> Categorias { get; set; }
   public DbSet<Proveedor> Proveedores { get; set; }
   public DbSet<InventarioMovimientos> InventarioMovimientos { get; set; }
   public DbSet<InventarioStock> InventarioStock { get; set; }
   public DbSet<TipoMovimiento>  TipoMovimiento { get; set; }
}