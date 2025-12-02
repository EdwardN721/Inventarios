using Inventarios.Business.Interface.Services;
using Inventarios.Business.Service;
using Inventarios.Bussiness.Interface.Repository;
using Inventarios.Infrastructure.Context;
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
        
        return services;
    }
}