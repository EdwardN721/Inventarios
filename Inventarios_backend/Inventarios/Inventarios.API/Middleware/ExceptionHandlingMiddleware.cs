using Npgsql;
using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Inventarios.Business.Exceptions;

namespace Inventarios.Middleware;

/// <summary>
/// Middleware global para atrapar todas las excepciones no controladas
/// y convertirlas en respuestas HTTP estandar
/// </summary>
public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    /// <summary>
    /// Constructor <see cref="ExceptionHandlingMiddleware"/>
    /// </summary>
    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    /// <summary>
    /// LLama a los eventos del middleware
    /// </summary>
    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext); // Llama al elemento siguiente del middelware (Controller, Service, etc.)
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ocurrió una excepción no controlada.");
            await HandleExceptionAsync(httpContext, ex);
        }
    }

    /// <summary>
    /// Convierte la excepcion en una respuesta HTTP JSON.
    /// </summary>
    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        HttpStatusCode statusCode;
        object errorResponse;

        switch (exception)
        {
            case ValidationException validationException:
                statusCode = HttpStatusCode.BadRequest;
                errorResponse = new { title = "Error de validación.", errors = validationException.Errors };
                break;
            case NotFoundException notFoundException:
                statusCode = HttpStatusCode.NotFound;
                errorResponse = new { title = "No encontrado.", errors = notFoundException.Message };
                break;
            case DbUpdateException dbUpdateException:
                statusCode = HttpStatusCode.BadRequest;
                errorResponse = new { title = "Error interno.", errors = "No se pudieron guardar los cambios." };
                if (dbUpdateException.InnerException is PostgresException postgresException)
                {
                    switch (postgresException.SqlState)
                    {
                        case "23505": 
                            statusCode = HttpStatusCode.Conflict;
                            errorResponse = new { title = "Conflicto.", errors = "Ya éxiste uin registro con estos datos." };
                            break;
                        case "23503":
                            statusCode = HttpStatusCode.BadRequest;
                            errorResponse = new
                            {
                                title = "Error de integridad.",
                                errors = "Estás intentando relacionar un registro que no existe."
                            };
                            break;
                        case "23502":
                            statusCode = HttpStatusCode.BadRequest;
                            errorResponse = new
                            {
                                title = "Datos incompletos.", 
                                errors = $"El campo '{postgresException.ColumnName}' es obligatorio y no se envió."
                            };
                        break;
                    }
                }
                break;
            
            default:
                statusCode = HttpStatusCode.InternalServerError;
                errorResponse = new { title = "Error interno.", errors = "Ocurrio un error inesperado." };
                break;
        }

        context.Response.StatusCode = (int)statusCode;
        return context.Response.WriteAsync(JsonSerializer.Serialize(errorResponse));
    }
}