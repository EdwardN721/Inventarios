using Inventarios.Business.Interface.Services;
using Inventarios.DTOs.DTO.Request;
using Inventarios.DTOs.DTO.Response;
using Microsoft.AspNetCore.Http;
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
    private readonly IInventarioMovimientoService _service;

    /// <summary>
    /// Controllador del movimiento de inventario 
    /// </summary>
    /// <param name="logger">Logger</param>
    /// <param name="service">Lógica del negocio</param>
    /// <exception cref="ArgumentNullException">Excepcion</exception>
    public InventarioMovimientoController(ILogger<InventarioMovimientoController> logger, IInventarioMovimientoService service)
    {
        _logger = logger;
        _service = service ?? throw new ArgumentNullException(nameof(service));
    }

    /// <summary>
    /// Obtener todas los movimientos
    /// </summary>
    /// <returns>Lista de movimientos</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<InventarioMovimientosResponseDto>))]
    public async Task<IActionResult> ObtenerMovimientos()
    {
        _logger.LogInformation("Obtener todos los movimientos");
        IEnumerable<InventarioMovimientosResponseDto> movimientos = await _service.ObtenerMovimientos();
        return Ok(movimientos);
    }

    /// <summary>
    /// Obtener movimiento
    /// </summary>
    /// <param name="id">Id del movimiento a buscar</param>
    /// <returns>Movimiento</returns>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(InventarioMovimientosResponseDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ObtenerMovimientoPorId([FromRoute] Guid id)
    {
        _logger.LogInformation("Obteniendo movimiento por Id: {Id}", id);
        InventarioMovimientosResponseDto movimiento = await _service.ObtenerMovimientoPorId(id);
        return Ok(movimiento);
    }

    /// <summary>
    /// Crear movimiento
    /// </summary>
    /// <param name="requestDto">Dto de movimiento</param>
    /// <returns>Movimiento creado</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(InventarioMovimientosResponseDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CrearMovimiento([FromBody] InventarioMovimientosRequestDto requestDto)
    {
        _logger.LogInformation("Creando movimiento: {Fecha}", requestDto.FechaMovimiento);
        InventarioMovimientosResponseDto movimiento = await _service.CrearMovimiento(requestDto);
        return CreatedAtAction(nameof(ObtenerMovimientoPorId), new { id = movimiento.Id }, movimiento);
    }

    /// <summary>
    /// Actualizar un movimiento
    /// </summary>
    /// <param name="id">Id del movimiento a actualizar</param>
    /// <param name="requestDto">Dto del movimiento a actualizar</param>
    /// <returns>Estado del movimiento</returns>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ActualizarMovimiento([FromRoute] Guid id,
        [FromBody] InventarioMovimientosRequestDto requestDto)
    {
        _logger.LogInformation("Actualizando movimiento Id: {Id}", id);
        await _service.ActualizarMovimiento(id, requestDto);
        return NoContent();
    }

    /// <summary>
    /// Eliminar movimiento
    /// </summary>
    /// <param name="id">Id del movimiento a actualizar</param>
    /// <returns>Estado del movimiento eliminado</returns>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> EliminarMovimiento([FromRoute] Guid id)
    {
        _logger.LogInformation("Eliminando movimiento por Id: {Id}", id);
        await _service.EliminarMovimiento(id);
        return NoContent();
    }
}

