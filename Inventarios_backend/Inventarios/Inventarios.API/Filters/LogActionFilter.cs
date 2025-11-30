using Microsoft.AspNetCore.Mvc.Filters;

namespace Inventarios.Filters;

/// <summary>
/// Filtro de Acción que loguea la ejecución de un metodo del controlador
/// </summary>
public class LogActionFilter : IActionFilter
{
    private readonly ILogger<LogActionFilter> _logger;

    public LogActionFilter(ILogger<LogActionFilter> logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// Se ejecuta antes de que la acción se ejecute
    /// </summary>
    public void OnActionExecuting(ActionExecutingContext context)
    {
        var controller = context.RouteData.Values["controller"];
        var action = context.RouteData.Values["action"];
        
        _logger.LogInformation($"Ejecutando: {controller} - {action}",  controller, action);
    }

    /// <summary>
    /// Se ejecuta despues de que la acción se completó
    /// </summary>
    public void OnActionExecuted(ActionExecutedContext context)
    {
        var controller = context.RouteData.Values["controller"];    
        var action = context.RouteData.Values["action"];

        if (context.Exception == null)
        {
            _logger.LogInformation($" --- Completado: {controller} - {action} ---", controller, action);
        }
        else
        {
            _logger.LogWarning(context.Exception, $" --- Falló: {controller} - {action} ---", controller, action);
        }
    }
}