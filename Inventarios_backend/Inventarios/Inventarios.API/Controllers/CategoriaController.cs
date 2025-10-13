using AutoMapper;
using Inventarios.DTOs.DTO.Categoria;
using Inventarios.Entities.Models;
using Inventarios.Infrastructure.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Inventarios.Controllers;

[ApiController]
[Route("[controller]")]
public class CategoriaController : ControllerBase
{
    private readonly ILogger<CategoriaController> _logger;
    private readonly ICategoriasRepository _repository;
    private readonly IMapper _mapper;
    
    public CategoriaController(ILogger<CategoriaController> logger, ICategoriasRepository repository, IMapper mapper)
    {
        _logger = logger ??  throw new ArgumentNullException(nameof(logger));
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Categorias>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> ObtenerCategorias()
    {
        _logger.LogInformation("Ejecutando metodo: {}", nameof(ObtenerCategorias));
        try
        {
            var items = await _repository.ObtenerTodosAsync();
            var categoriasDto = _mapper.Map<IEnumerable<Categorias>>(items);
            
            return Ok(categoriasDto);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ocurrió un error inesperado al obtener las categorías.");
            return StatusCode(500, new { error = "Ocurrió un error interno. Por favor, intente más tarde." });
        }
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Categorias))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ObtenerCategoriaPorId(int id)
    {
        _logger.LogInformation("Ejecutando metodo: {}", nameof(ObtenerCategoriaPorId));
        try
        {
            var item = await _repository.ObtenerPorIdAsync(id);

            if (item is null)
            {
                return NotFound($"Categoria con el ID: {id} no encontrada.");
            }
            
            var categoriaDto = _mapper.Map<Categorias>(item);
            
            return Ok(categoriaDto);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ocurrió un error inesperado al obtener la categoría: {}.", id);
            return StatusCode(500, new { error = "Ocurrió un error interno. Por favor, intente más tarde." });
        }
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Categorias))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> CrearCategorias(CategoriaToCreateDto  categoriaDto)
    {
        _logger.LogInformation("Ejecutando metodo: {}", nameof(CrearCategorias));
        try
        {
            var categoriaToCreated = _mapper.Map<Categorias>(categoriaDto);
            categoriaToCreated.CreatedAt = DateTime.UtcNow;
            var categoria = await _repository.AgregarRegistroAsync(categoriaToCreated); 
            return Ok(categoria);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ocurrio un error inesperado al agregar la Categoria");
             return StatusCode(500, new { error = "Ocurrió un error interno. Por favor, intente más tarde." });
        }
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ActualizarCategorias(CategoriaToUpdateDto categoriaDto)
    {
        _logger.LogInformation("Ejecutando método: {}", nameof(ActualizarCategorias));
        try
        {
            if (categoriaDto.Id == 0)
            {
                _logger.LogWarning("Id inválido.");
                return BadRequest("Error al intentar actualizar la Categoria.");
            }
            
            var categoriaToUpdate = await _repository.ObtenerPorIdAsync(categoriaDto.Id);
            if (categoriaToUpdate is null)
            {
                _logger.LogWarning("Categoria no encontrada");
                return NotFound(new { error = "Categoria no encontrada." });
            }
            
            _mapper.Map(categoriaDto, categoriaToUpdate);
            
            categoriaToUpdate.UpdatedAt = DateTime.UtcNow;
            var actualizado = await _repository.ActualizarRegistroAsync(categoriaToUpdate.Id, categoriaToUpdate);

            if (!actualizado)
            {
                return NoContent();
            }
            
            var categoria = await _repository.ObtenerPorIdAsync(categoriaToUpdate.Id);
            var list = _mapper.Map<Categorias>(categoria);

            return Ok(list);
        }
        catch (Exception ex)
        {
                _logger.LogError(ex, "Ocurrio un error inesperado al actualizar la Categoria");
                return StatusCode(500, new { error = "Ocurrió un error interno. Por favor, intente más tarde." });
        }
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> EliminarCategorias(int id)
    {
        _logger.LogInformation("Ejecutando método: {}", nameof(EliminarCategorias));
        try
        {
            var categoriaToDelete = await _repository.ObtenerPorIdAsync(id);

            if (categoriaToDelete is null)
            {
                return NotFound("Categotía no encontrada.");
            }

            var deleted = await _repository.EliminarRegistroAsync(categoriaToDelete);

            if (!deleted)
            {
                return Ok("Registro no eliminado.");
            }
        
            return Ok("Registro eliminado.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ocurrio un error inesperado al eliminar la Categoria");
            return StatusCode(500, new { error = "Ocurrió un error interno. Por favor, intente más tarde." });
        }
    }
}