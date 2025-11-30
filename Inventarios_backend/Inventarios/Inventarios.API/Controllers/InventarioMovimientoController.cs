using Microsoft.AspNetCore.Mvc;

namespace Inventarios.Controllers;

/// <summary>
/// Controlador para gestionar las operaciones del Movimiento del inventario. 
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class InventarioMovimientoController : ControllerBase
{
    private readonly ILogger<InventarioMovimientoController> _logger;
    //private readonly IInventario
}

