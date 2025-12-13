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
       
       // SEED - Agregar Tipo de movimientos
       modelBuilder.Entity<TipoMovimiento>().HasData(
        // 1. ENTRADAS
        new TipoMovimiento 
        { 
            Id = 1,
            Nombre = "Compra a Proveedor",
            Descripcion = "Entrada de mercancía por orden de compra",
            EsEntrada = true,
            EsSalida = false,
            EsTransferenciaInterna = false,
            CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc)
        },
        new TipoMovimiento 
        { 
            Id = 2, 
            Nombre = "Devolución de Cliente",
            Descripcion = "Reingreso de productos vendidos",
            EsEntrada = true, EsSalida = false, EsTransferenciaInterna = false,
            CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc)
        },
        new TipoMovimiento 
        { 
            Id = 3, 
            Nombre = "Ajuste de Inventario (+)",
            Descripcion = "Corrección positiva por sobrantes",
            EsEntrada = true, EsSalida = false, EsTransferenciaInterna = false,
            CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc)
        },

        // 2. SALIDAS
        new TipoMovimiento 
        { 
            Id = 4, 
            Nombre = "Venta Directa",
            Descripcion = "Salida de mercancía por venta",
            EsEntrada = false, EsSalida = true, EsTransferenciaInterna = false,
            CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc)
        },
        new TipoMovimiento 
        { 
            Id = 5, 
            Nombre = "Devolución a Proveedor",
            Descripcion = "Salida de mercancía defectuosa",
            EsEntrada = false, EsSalida = true, EsTransferenciaInterna = false,
            CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc)
        },
        new TipoMovimiento 
        { 
            Id = 6, 
            Nombre = "Ajuste de Inventario (-)",
            Descripcion = "Corrección negativa por faltantes o merma",
            EsEntrada = false, EsSalida = true, EsTransferenciaInterna = false,
            CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc)
        },
        new TipoMovimiento 
        { 
            Id = 7, 
            Nombre = "Baja por Caducidad",
            Descripcion = "Salida de productos expirados",
            EsEntrada = false, EsSalida = true, EsTransferenciaInterna = false,
            CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc)
        },

        // 3. TRANSFERENCIAS
        new TipoMovimiento 
        { 
            Id = 8, 
            Nombre = "Transferencia entre Almacenes",
            Descripcion = "Cambio de ubicación",
            EsEntrada = false, EsSalida = false, EsTransferenciaInterna = true,
            CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc)
        }
    );
   }
}