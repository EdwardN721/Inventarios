using Inventarios.Bussiness.Exceptions;
using Inventarios.Bussiness.Interface.Services;
using Inventarios.DTOs.DTO.Request;
using Inventarios.DTOs.DTO.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Inventarios.Controllers;

/// <summary>
/// Controlador para gestionar las operaciones de Inventario Stock
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class InventarioStockController : ControllerBase
{
    private readonly ILogger<InventarioStockController> _logger;
    private readonly IInventarioStockService _service;

    /// <summary>
    /// Contructor de <see cref="InventarioStockController"/>
    /// </summary>
    /// <param name="logger">Administrador de logs</param>
    /// <param name="service">Interface de Service</param>
    public InventarioStockController(ILogger<InventarioStockController> logger, IInventarioStockService service)
    {
        _logger = logger;
        _service = service ?? throw new ArgumentNullException(nameof(service));
    }

    /// <summary>
    /// Obtiene todos los Stock de inventario
    /// </summary>
    /// <returns>Lista de Stocks</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<InventarioStockResponseDto>))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> ObtenerTodoStock()
    {
        _logger.LogInformation("Obteniendo todo el stock");
        try
        {
            IEnumerable<InventarioStockResponseDto> inventario = await _service.ObtenerInventariosStocks();

            _logger.LogInformation("Stock obtenido: {stock}", inventario.Count());
            return Ok(inventario);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    /// <summary>
    /// Obtener Stock Por Id
    /// </summary>
    /// <param name="id">Id del stock a buscar</param>
    /// <returns>Stock</returns>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(InventarioStockResponseDto))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> ObtenerStockPorId([FromQuery] Guid id)
    {
        _logger.LogInformation("Obteniendo stock {Id}", id);
        try
        {
            InventarioStockResponseDto stock = await _service.ObtenerInventarioStockPorId(id);

            _logger.LogInformation("Stock encontrado.");
            return Ok(stock);
        }
        catch (NotFoundException ex)
        {
            _logger.LogError(ex, ex.Message);
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    /// <summary>
    /// Crea un stock de inventario
    /// </summary>
    /// <param name="inventarioStockRequestDto">Dto para crear el stock</param>
    /// <returns>Respuesta de la petición</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(InventarioStockResponseDto))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CrearStock([FromBody] InventarioStockRequestDto inventarioStockRequestDto)
    {
        _logger.LogInformation("Creando stock");
        try
        {
            InventarioStockResponseDto stock = await _service.InsertarInventarioStock(inventarioStockRequestDto);

            _logger.LogInformation("Stock creado.");
            return CreatedAtAction(nameof(ObtenerStockPorId), new { id = stock.Id }, stock);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    /// <summary>
    /// Actualiza un stock
    /// </summary>
    /// <param name="id">Id del stock a actualizar</param>
    /// <param name="inventarioStockRequestDto">Dto para modificar al inventario</param>
    /// <returns>Respuesta de la petición</returns>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> ActualizarInventario([FromQuery] Guid id,
        InventarioStockRequestDto inventarioStockRequestDto)
    {
        _logger.LogInformation("Actualizando stock {Id}", id);
        try
        {
            await _service.ActualizarInventarioStock(id, inventarioStockRequestDto);

            _logger.LogInformation("Stock actualizado.");
            return NoContent();
        }
        catch (NotFoundException ex)
        {
            _logger.LogError(ex, ex.Message);
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    /// <summary>
    /// Eliminando un stock
    /// </summary>
    /// <param name="id">Id del inventario a eliminar</param>
    /// <returns>Respuesta Htttp</returns>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> EliminarStock(Guid id)
    {
        _logger.LogInformation("Eliminando stock {Id}", id);
        try
        {
            await _service.EliminarInventarioStock(id);

            _logger.LogInformation("Stock eliminado.");
            return NoContent();
        }
        catch (NotFoundException ex)
        {
            _logger.LogError(ex, ex.Message);
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
}