using Inventarios.Business.Interface.Services;
using Inventarios.DTOs.DTO.Request;
using Inventarios.DTOs.DTO.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Inventarios.Controllers;

/// <summary>
/// Controlador para administrar Tipo movimientos
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class TipoMovimientoController : ControllerBase
{
    private readonly ILogger<TipoMovimientoController> _logger;
    protected readonly ITipoMovimientoService _service;

    /// <summary>
    /// Constructor de <see cref="TipoMovimientoController"/>
    /// </summary>
    /// <param name="logger">Registro</param>
    /// <param name="service">Lógica del negocio</param>
    public TipoMovimientoController(ILogger<TipoMovimientoController> logger, ITipoMovimientoService service)
    {
        _logger = logger;
        _service = service ?? throw new ArgumentNullException(nameof(service));
    }

    /// <summary>
    /// Obtener tipos de movimientos
    /// </summary>
    /// <returns>Lista de tipo de movimientos</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<TipoMovimientoResponseDto>))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> ObtenerTipoMovimientos()
    {
        _logger.LogInformation("Obteniendo tipo de Movimientos");
        IEnumerable<TipoMovimientoResponseDto> tipoMovimientos = await _service.ObtenerTodosTiposMovimientos();
        return Ok(tipoMovimientos);
    }

    /// <summary>
    /// Obtener tipo de movimiento por Id
    /// </summary>
    /// <param name="id">Id del tipo de movimiento</param>
    /// <returns>Tipo de movimiento</returns>
    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TipoMovimientoResponseDto))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> ObtenerTipoMovimientoPorId([FromRoute] int id)
    {
        _logger.LogInformation("Obteniendo tipo de Movimiento Id {id}", id);
        TipoMovimientoResponseDto tipoMovimiento = await _service.ObtenerTipoMovimientoPorId(id);
        return Ok(tipoMovimiento);
    }

    /// <summary>
    /// Agregando tipo de movimiento
    /// </summary>
    /// <param name="tipoMovimientoRequestDto">Dto de tipo movimiento</param>
    /// <returns>Tipo de movimiento agregado</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(TipoMovimientoResponseDto))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> AgregarTipoMovimiento([FromBody] TipoMovimientoRequestDto tipoMovimientoRequestDto)
    {
        _logger.LogInformation("Creando tipo de Movimiento: {Nombre}", tipoMovimientoRequestDto.Nombre);
        TipoMovimientoResponseDto tipoMovimiento = await _service.AgregarTipoMovimiento(tipoMovimientoRequestDto);
        
        return CreatedAtAction(nameof(ObtenerTipoMovimientoPorId), new{id = tipoMovimiento.Id}, tipoMovimiento);
    }

    /// <summary>
    /// Actualizar tipo de movimiento
    /// </summary>
    /// <param name="id">Id del tipo de movimiento a actualizar</param>
    /// <param name="tipoMovimientoRequestDto">Dto de tipo de movimiento</param>
    /// <returns>Estado de la actualizacion</returns>
    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> ActualizarTipoMovimiento([FromRoute] int id,
        [FromBody] TipoMovimientoRequestDto tipoMovimientoRequestDto)
    {
        _logger.LogInformation("Actualizando tipo movimiento {Id}", id);
        await _service.ActualizarTipoMovimiento(id, tipoMovimientoRequestDto);
        return NoContent();
    }

    /// <summary>
    /// Eliminar tipo de movimiento
    /// </summary>
    /// <param name="id">Id del tipo de movimiento a eliminar</param>
    /// <returns>Estado de la eliminacion</returns>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> EliminarTipoMovimiento([FromRoute] int id)
    {
        _logger.LogWarning("Elimnando Tipo de movimiento {id}", id);
        await  _service.EliminarTipoMovimiento(id);
        return NoContent();
    }
    
}