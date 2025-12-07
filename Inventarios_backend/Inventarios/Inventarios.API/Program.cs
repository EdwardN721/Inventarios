using FluentValidation.AspNetCore;
using Inventarios.Extensions;
using Inventarios.Filters;
using Inventarios.Infrastructure;
using Inventarios.Middleware;
using Microsoft.AspNetCore.Builder;
using Serilog;

// Configuración inicial del Logger
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console()
    .WriteTo.File("logs/inventario-log-.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

try
{
    var builder = WebApplication.CreateBuilder(args);

    // 1. Configurar Serilog como el Logger del Host
    builder.Host.UseSerilog();

    // 2. Capa de Infraestructura (Base de Datos y Repositorios)
    builder.Services.AddContextPostgressServer(builder.Configuration, "DefaultConnection");
    builder.Services.AddInfrastructure();
    
    // 3. Capa de Negocio (Servicios)
    builder.Services.AddBusiness();

    // 4. Capa de Presentación (Controladores y Swagger)
    builder.Services.AddControllers();
    builder.Services.AddSwagger();
    
    // 5. Validaciones y Filtros
    builder.Services.AddValidator();
    builder.Services.AddFluentValidationAutoValidation();
    
    builder.Services.AddScoped<LogActionFilter>();
    builder.Services.AddControllers(opt =>
        opt.Filters.Add<LogActionFilter>());
    
    var app = builder.Build();

    // --- Configuración del Pipeline HTTP ---

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    // Middleware global de manejo de errores
    app.UseMiddleware<ExceptionHandlingMiddleware>();
    
    app.UseHttpsRedirection();
    app.UseAuthorization();
    app.MapControllers();
    
    app.Run();
}
catch (Exception ex) when (ex is not HostAbortedException)
{
    Log.Fatal(ex, "La aplicación falló al iniciar.");
}
finally
{
    Log.CloseAndFlush(); 
}