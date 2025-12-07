using Inventarios.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Inventarios.Infrastructure.Context;

public class InventariosDbContext : DbContext
{
    public InventariosDbContext(DbContextOptions<InventariosDbContext> options)  : base(options)
    {
        
    }
    
   public DbSet<Producto> Productos { get; set; }
   public DbSet<Categoria> Categorias { get; set; }
   public DbSet<Proveedor> Proveedores { get; set; }
   public DbSet<InventarioMovimiento> InventarioMovimientos { get; set; }
   public DbSet<InventarioStock> InventarioStocks { get; set; }
   public DbSet<TipoMovimiento>  TipoMovimientos { get; set; }

   protected override void OnModelCreating(ModelBuilder modelBuilder)
   {
       base.OnModelCreating(modelBuilder);
       
       // --- Configuración de CATEGORIAS ---
       modelBuilder.Entity<Categoria>(entity =>
       {
           entity.ToTable("Categorias");
           entity.HasKey(e => e.Id);
           entity.Property(e => e.CreatedAt)
               .HasDefaultValueSql("now()");
       });

       // --- Configuración de PRODUCTOS ---
       modelBuilder.Entity<Producto>(entity =>
       {
           entity.ToTable("Productos");
           entity.HasKey(e => e.Id);
           
           entity.Property(e => e.Id)
               .HasDefaultValueSql("gen_random_uuid()");
            
           entity.Property(e => e.CreatedAt)
               .HasDefaultValueSql("now()");
       });

      
       modelBuilder.Entity<Proveedor>(entity =>
       {
           entity.ToTable("Proveedores");
           entity.Property(e => e.Id).HasDefaultValueSql("gen_random_uuid()");
           entity.Property(e => e.CreatedAt).HasDefaultValueSql("now()");
       });

       // --- Configuración de INVENTARIO MOVIMIENTOS ---
       modelBuilder.Entity<InventarioMovimiento>(entity =>
       {
           entity.ToTable("InventarioMovimientos");
           entity.Property(e => e.Id).HasDefaultValueSql("gen_random_uuid()");
           entity.Property(e => e.FechaMovimiento).HasDefaultValueSql("now()"); 
       });
        
       // --- Configuración de STOCK ---
       modelBuilder.Entity<InventarioStock>(entity =>
       {
           entity.ToTable("InventarioStock");
           entity.Property(e => e.Id).HasDefaultValueSql("gen_random_uuid()");
           entity.Property(e => e.CreatedAt).HasDefaultValueSql("now()");
       });
   }
}