using System.Net;
using System.Text.Json;
using Inventarios.Business.Exceptions;
using Microsoft.AspNetCore.Http;

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
            default:
                statusCode = HttpStatusCode.InternalServerError;
                errorResponse = new { title = "Error interno.", errors = "Ocurrio un error inesperado." };
                break;
        }

        context.Response.StatusCode = (int)statusCode;
        return context.Response.WriteAsync(JsonSerializer.Serialize(errorResponse));
    }
}