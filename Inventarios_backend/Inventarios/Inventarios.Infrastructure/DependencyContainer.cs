using Inventarios.Infrastructure.Context;
using Inventarios.Infrastructure.Interface;
using Inventarios.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Inventarios.Infrastructure;

public static class DependencyContainer
{
    public static IServiceCollection AddContextPostgressServer(
        this IServiceCollection services,
        IConfiguration configuration,
        string connectionString
    )
    {
        services.AddDbContext<InventariosDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString(connectionString)));
        return services;
    }

    public static IServiceCollection AddRepositories(
        this IServiceCollection services)
    {
        services.AddScoped<ICategoriasRepository, CategoriasRepository>();
        services.AddScoped<IInventarioMovimientosRepository, InventarioMovimientosRepository>();
        services.AddScoped<IInventatioStockRepository, InventarioStockRepository>();
        services.AddScoped<ITipoMovimientoRepository,TipoMovimientoRepository>();
        services.AddScoped<IProductosRepository, ProductosRepository>();
        services.AddScoped<IProveedorRepository, ProveedorRepository>();
        
        return services;
    }
}