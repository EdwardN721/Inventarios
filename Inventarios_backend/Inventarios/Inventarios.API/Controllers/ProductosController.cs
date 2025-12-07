using Inventarios.Business.Interface.Services;
using Inventarios.DTOs.DTO.Request;
using Inventarios.DTOs.DTO.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Inventarios.Controllers;

/// <summary>
/// Controlador para gestionar los productos
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class ProductosController : ControllerBase
{
    private readonly ILogger<ProductosController> _logger;
    private readonly IProductoService _service;

    /// <summary>
    /// Constructor del controlador de productos
    /// </summary>
    /// <param name="logger">Ilogger </param>
    /// <param name="service">Lógica del negocio</param>
    /// <exception cref="ArgumentNullException">Excepcion atrapada</exception>
    public ProductosController(ILogger<ProductosController> logger, IProductoService service)
    {
        _logger = logger;
        _service = service ?? throw new ArgumentNullException(nameof(service));
    }

    /// <summary>
    /// Obtener lista de productos
    /// </summary>
    /// <returns>Lista de productos</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<InventarioMovimientosResponseDto>))]
    public async Task<IActionResult> ObtenerProductos()
    {
        IEnumerable<ProductoResponseDto> productos = await _service.ObtenerProductos(); 
        return Ok(productos);
    }

    /// <summary>
    /// Obtener un producto por su Id
    /// </summary>
    /// <param name="id">Id del producto</param>
    /// <returns>Producto</returns>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(InventarioMovimientosResponseDto))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> ObtenerProductoPorId([FromRoute] Guid id)
    {
        ProductoResponseDto prducto = await _service.ObtenerProductoPorId(id);
        return Ok(prducto);
    }

    /// <summary>
    /// Agregar producto
    /// </summary>
    /// <param name="productoRequestDto">Dto de producto</param>
    /// <returns>Producto agregado</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(InventarioMovimientosResponseDto))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> AgregarProducto([FromBody] ProductoRequestDto productoRequestDto)
    {
        ProductoResponseDto nuevoProducto = await _service.AgregarProducto(productoRequestDto);
        return CreatedAtAction(nameof(ObtenerProductoPorId), new {id = nuevoProducto.Id}, nuevoProducto);
    }

    /// <summary>
    /// Actualizar producto 
    /// </summary>
    /// <param name="id">Id del producto</param>
    /// <param name="productoRequestDto">Dto producto</param>
    /// <returns>Estado de actualizacion</returns>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> ActualizarProducto([FromRoute] Guid id,
        [FromBody] ProductoRequestDto productoRequestDto)
    {
        await _service.ActualizarProducto(id, productoRequestDto);
        return NoContent();
    }

    /// <summary>
    /// Eliminar producto
    /// </summary>
    /// <param name="id">Id del producto a eliminar</param>
    /// <returns>Estado de eliminacion</returns>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> EliminarProducto([FromRoute] Guid id)
    {
        await _service.EliminarProducto(id);
        return NoContent();
    }
}