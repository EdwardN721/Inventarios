using Inventarios.Business.Interface.Services;
using Inventarios.DTOs.DTO.Request;
using Inventarios.DTOs.DTO.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Inventarios.Controllers;

/// <summary>
/// Controlador para administrar a los proveedores
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class ProveedorController : ControllerBase
{
    private readonly ILogger<ProveedorController> _logger;
    private readonly IProveedorService _service;

    /// <summary>
    /// Constructor del controlador de Proveedor 
    /// </summary>
    /// <param name="logger">Registro del logeo</param>
    /// <param name="service">Lógica del negocio</param>
    /// <exception cref="ArgumentNullException">Excepcion</exception>
    public ProveedorController(ILogger<ProveedorController> logger, IProveedorService service)
    {
        _logger = logger;
        _service = service ?? throw new ArgumentNullException(nameof(service));
    }

    /// <summary>
    /// Obtener proveedores
    /// </summary>
    /// <returns>Lista de proveedores</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ProveedorResponseDto>))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> ObtenerProveedores()
    {
        _logger.LogInformation("Obteniendo proveedores.");
        IEnumerable<ProveedorResponseDto> proveedores = await _service.ObtenerProveedores();
        return Ok(proveedores);
    }

    /// <summary>
    /// Obteniendo proveedor por Id
    /// </summary>
    /// <param name="id">Id del Proveedor</param>
    /// <returns>Un proveedor</returns>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProveedorResponseDto))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> ObtenerProveedorPorId([FromRoute] Guid id)
    {
        _logger.LogInformation("Obteniendo proveedor: {Id}.", id);
        ProveedorResponseDto proveedor = await _service.ObtenerProveedorPorId(id);
        return Ok(proveedor);
    }

    /// <summary>
    /// Creando proveedor
    /// </summary>
    /// <param name="proveedorDto">Dto del proveedor</param>
    /// <returns>Proveedor creado</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ProveedorResponseDto))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CrearProveedor([FromBody] ProveedorRequestDto proveedorDto)
    {
        _logger.LogInformation("Creando proveedor {Nombre}.", proveedorDto.Nombre);
        ProveedorResponseDto proveedor = await _service.GuardarProveedor(proveedorDto);
        return CreatedAtAction(nameof(ObtenerProveedorPorId), new {id = proveedor.Id}, proveedor);
    }
    
    /// <summary>
    /// Actualizando proveedor
    /// </summary>
    /// <param name="id">Id del proveedor a actualizar</param>
    /// <param name="proveedorDto">Dto del proveedor</param>
    /// <returns>Estado de la actualizacion</returns>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> ActualizarProveedor([FromRoute] Guid id, [FromBody] ProveedorRequestDto proveedorDto)
    {
        _logger.LogInformation("Actualizando proveedor {Id}", id);
        await _service.ActualizarProveedor(id, proveedorDto);
        return NoContent();
    }

    /// <summary>
    /// Eliminar proveedor
    /// </summary>
    /// <param name="id">ID del proveedor a eliminar</param>
    /// <returns>Estado de la eliminacion</returns>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> EliminarProveedor([FromRoute] Guid id)
    {
        _logger.LogWarning("Eliminando proveedor {Id}", id);
        await _service.EliminarProveedor(id);
        return NoContent();
    }

}