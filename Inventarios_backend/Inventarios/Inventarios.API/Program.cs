using FluentValidation.AspNetCore;
using Inventarios.Extensions;
using Inventarios.Filters;
using Inventarios.Infrastructure;
using Inventarios.Middleware;
using Microsoft.AspNetCore.Builder;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console()
    .WriteTo.File("logs/inventario-log-.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Services.AddContextPostgressServer(builder.Configuration, "DefaultConnection");
    builder.Services.AddInfrastructure();
    builder.Services.AddBusiness();

    builder.Services.AddControllers();

    // Agregar swagger Swashbukle.AspNetCore

    builder.Services.AddSwagger();
    
    // Agregar Validator
    builder.Services.AddValidator();
    builder.Services.AddFluentValidationAutoValidation();
    
    
    //Agregar Middleware y Filters
    builder.Services.AddScoped<LogActionFilter>();
    builder.Services.AddControllers(opt =>
        opt.Filters.Add<LogActionFilter>());
    
    var app = builder.Build();

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
} catch (Exception ex)
{
    Log.Fatal(ex, "La aplicación falló al iniciar.");
}
finally
{
    Log.CloseAndFlush(); 
}