using Inventarios.Bussiness.Service;
using Inventarios.DTOs.DTO.Request;
using Inventarios.DTOs.DTO.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Inventarios.Controllers;

/// <summary>
/// Controlador para gestionar las operaciones de Categoria
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class CategoriaController : ControllerBase
{
    private readonly ILogger<CategoriaController> _logger;
    private readonly ICategoriaService _service;
    
    /// <summary>
    /// Constructor de <see cref="CategoriaController"/>
    /// </summary>
    /// <param name="logger">Logs del controller</param>
    /// <param name="service">Reglas del negocio</param>
    /// <exception cref="ArgumentNullException">Excepcion controlada</exception>
    public CategoriaController(ILogger<CategoriaController> logger, ICategoriaService service)
    {
        _logger = logger ??  throw new ArgumentNullException(nameof(logger));
        _service = service ?? throw new ArgumentNullException(nameof(service));
    }

    /// <summary>
    /// Obtiene todas las categorias
    /// </summary>
    /// <returns>Una lista de categorias</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<CategoriaResponseDto>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> ObtenerCategorias()
    {
        _logger.LogInformation("Obteniendo categorias.");
        try
        {
            IEnumerable<CategoriaResponseDto> categorias = await _service.ObtenerCategorias();
            
            _logger.LogInformation("Categorias obtenidas: {}",  categorias.Count());
            return Ok(categorias);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ocurrió un error inesperado al obtener las categorías.");
            return StatusCode(500, new { error = "Ocurrió un error interno. Por favor, intente más tarde." });
        }
    }

    /// <summary>
    /// Obtiene una Categoria por su Id
    /// </summary>
    /// <param name="id">Id de la categoria (int)</param>
    /// <returns>Una categoria</returns>
    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CategoriaResponseDto))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ObtenerCategoriaPorId(int id)
    {
        _logger.LogInformation("Obteniendo categoria con el Id: {}.", id);
        try
        {
            CategoriaResponseDto categoria = await _service.ObtenerCategoriaPorId(id); 
            
            _logger.LogInformation("Categoria con el Id: {} encontrada.", id);
            return Ok(categoria);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ocurrió un error inesperado al obtener la categoría: {}.", id);
            return StatusCode(500, new { error = "Ocurrió un error interno. Por favor, intente más tarde." });
        }
    }

    /// <summary>
    /// Agrega una nueva categoria
    /// </summary>
    /// <param name="categoriaRequestDto">DTO de Categoria</param>
    /// <returns>Categoria</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CategoriaResponseDto))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CrearCategorias(CategoriaRequestDto categoriaRequestDto)
    {
        _logger.LogInformation("Creando categoria: {}", categoriaRequestDto.Nombre);
        try
        { 
            var categoriaDto = await _service.AgregarCategoria(categoriaRequestDto);
            
            _logger.LogInformation("Categoria agregada: ID {categoriaId} - Nombre {categoriaNombre}.", categoriaDto.Id, categoriaDto.Nombre);
            return CreatedAtAction(nameof(ObtenerCategoriaPorId), new { id = categoriaDto.Id }, categoriaDto);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ocurrio un error inesperado al agregar la Categoria");
             return StatusCode(500, new { error = "Ocurrió un error interno. Por favor, intente más tarde." });
        }
    }

    /// <summary>
    /// Actualizar una categoria 
    /// </summary>
    /// <param name="id">Id de categoria a actualziar (int)</param>
    /// <param name="categoriaDto">Categoria</param>
    /// <returns>Codigo de Estado de la actualizacion</returns>
    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> ActualizarCategorias(int id, CategoriaRequestDto categoriaDto)
    {
        _logger.LogInformation("Actualizando categoria: Id: {id} - {nombre}", id, categoriaDto.Nombre);
        try
        {
            if (id == 0)
            {
                _logger.LogWarning("Id inválido.");
                return BadRequest("Error al intentar actualizar la Categoria.");
            }
            
            await _service.ActualizarCategoria(id, categoriaDto);

            _logger.LogInformation("Categoria {} actualizada.", categoriaDto.Nombre);
            return NoContent();
        }
        catch (Exception ex)
        {
                _logger.LogError(ex, "Ocurrio un error inesperado al actualizar la Categoria");
                return StatusCode(500, new { error = "Ocurrió un error interno. Por favor, intente más tarde." });
        }
    }

    /// <summary>
    /// Eliminar una categoria
    /// </summary>
    /// <param name="id">Id de la categoria a eliminar (int)</param>
    /// <returns>Codigo de Estado eliminar</returns>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> EliminarCategorias(int id)
    {
        _logger.LogInformation("Ejecutando método: {}", nameof(EliminarCategorias));
        try
        {
            await _service.EliminarCategoria(id);
        
            _logger.LogInformation("Categoria {Id} eliminada.", id);
            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ocurrio un error inesperado al eliminar la Categoria");
            return StatusCode(500, new { error = "Ocurrió un error interno. Por favor, intente más tarde." });
        }
    }
}