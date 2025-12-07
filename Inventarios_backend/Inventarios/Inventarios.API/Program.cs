using FluentValidation.AspNetCore;
using Inventarios.Extensions;
using Inventarios.Filters;
using Inventarios.Infrastructure;
using Inventarios.Middleware;
using Microsoft.AspNetCore.Builder;
using Serilog;
using Microsoft.Extensions.Hosting;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console()
    .WriteTo.File("logs/inventario-log-.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

try
{
    var builder = WebApplication.CreateBuilder(args);

    // 1. Configurar Serilog primero (Buena práctica)
    builder.Host.UseSerilog();

    // 2. Servicios (SIN DUPLICADOS)
    builder.Services.AddContextPostgressServer(builder.Configuration, "DefaultConnection");
    builder.Services.AddInfrastructure();
    builder.Services.AddBusiness();
    
    builder.Services.AddControllers();

    // Swagger
    builder.Services.AddSwagger();
    
    // Validadores
    builder.Services.AddValidator();
    builder.Services.AddFluentValidationAutoValidation();
    
    // Filtros
    builder.Services.AddScoped<LogActionFilter>();
    builder.Services.AddControllers(opt =>
        opt.Filters.Add<LogActionFilter>());
    
    var app = builder.Build();

    // Configuración del Pipeline HTTP
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseMiddleware<ExceptionHandlingMiddleware>();
    
    app.UseHttpsRedirection();
    app.UseAuthorization();
    app.MapControllers();
    
    app.Run();
}
// 3. CORRECCIÓN CLAVE: Ignorar HostAbortedException
catch (Exception ex) when (ex is not HostAbortedException)
{
    Log.Fatal(ex, "La aplicación falló al iniciar.");
}
finally
{
    Log.CloseAndFlush(); 
}