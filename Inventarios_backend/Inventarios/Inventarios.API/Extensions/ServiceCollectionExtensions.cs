using System.Reflection;
using Microsoft.OpenApi.Models;
using FluentValidation;
using Inventarios.Business.Interface.Services;

namespace Inventarios.Extensions;

/// <summary>
/// Contenedor de dependencia para el swagger
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Genera el swagger
    /// </summary>
    public static IServiceCollection AddSwagger(
        this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Sistema Inventarios API", 
                Version = "v1", 
                Description =  "API para la gestión de inventarios.",
            });
            
            // Seguridad
            // Botion "Authorize" a la UI de Swagger
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "Escribe 'Bearer' [espacio] y luego tu token. \r\n\r\nEjemplo: \"Bearer abc123xyz\""
            });
            
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] { }
                }
            });
            
            var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFilename);
            options.IncludeXmlComments(xmlPath);
        });
        return services;
    }

    /// <summary>
    /// Agregar validaciones
    /// </summary>
    public static IServiceCollection AddValidator(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining<ICategoriaService>();
        
        return services;
    }
}