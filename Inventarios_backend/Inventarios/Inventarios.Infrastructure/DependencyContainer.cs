using Inventarios.Business.Service;
using Microsoft.EntityFrameworkCore;
using Inventarios.Infrastructure.Context;
using Microsoft.Extensions.Configuration;
using Inventarios.Infrastructure.Repository;
using Inventarios.Business.Interface.Services;
using Microsoft.Extensions.DependencyInjection;
using Inventarios.Business.Interface.Repository;

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

    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        return services;
    }

    public static IServiceCollection AddBusiness(this IServiceCollection services)
    {
        services.AddScoped<ICategoriaService, CategoriaService>();
        services.AddScoped<IInventarioStockService, InventarioStockService>();
        services.AddScoped<IInventarioMovimientoService, InventarioMovimientoService>();
        services.AddScoped<IProductoService, ProductoService>();
        services.AddScoped<IProveedorService, ProveedorService>();
        services.AddScoped<ITipoMovimientoService, TipoMovimientoService>();
        
        return services;
    }
}