/*using Inventarios.Bussiness.Mappers;
using Inventarios.DTOs.DTO.Request;
using Inventarios.DTOs.DTO.Response;
using Inventarios.Entities.Models;
using Inventarios.Infrastructure.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Inventarios.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InventarioMovimientoController : ControllerBase
{
    private readonly ILogger<InventarioMovimientoController> _logger;
    private readonly IInventarioMovimientosRepository _repository;

    public InventarioMovimientoController(ILogger<InventarioMovimientoController> logger, IInventarioMovimientosRepository repository)
    {
        _logger = logger;
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<InventarioMovimientosResponseDto>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> ObtenerInventarioMovimientos()
    {
        _logger.LogInformation("Obteniendo Movimientos del inventario");
        try
        {
            var items = await _repository.ObtenerTodosAsync();

            var movimientosDto = ManualMapperInventarioMovimientos.ToInventarioMovimientoResponse(items);

            _logger.LogInformation("Movimientos obtenidos {}", movimientosDto.Count());
            return Ok(movimientosDto);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ocurrió un error al obtener los movimientos del inventario");
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(InventarioMovimientosResponseDto))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ObtenerInventarioMovimientoPorId(Guid id)
    {
        _logger.LogInformation("Obteniendo movimiento por Id: {}", id);
        try
        {
            var item = await _repository.ObtenerPorIdAsync(id);

            if (item == null)
            {
                _logger.LogWarning("El movimiento con el Id: {} no existe", id);
                return NotFound();
            }

            var movimientoDto = ManualMapperInventarioMovimientos.ToInventarioMovimientoResponse(item);

            _logger.LogInformation("Inventario con el Id: {} encontrado.", id);
            return Ok(movimientoDto);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ocurrio un erro al obtener el Id {}", id);
            return StatusCode(500, "Internal server error");
        }
    }
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(InventarioMovimientosResponseDto))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CrearMovimiento(
        [FromBody] InventarioMovimientosRequestDto inventarioMovimientosRequestDto)
    {
         _logger.LogInformation("Creando movimiento: {}", inventarioMovimientosRequestDto.FechaMovimiento);
         try
         {
             InventarioMovimientos movimiento =
                 ManualMapperInventarioMovimientos.ToInventarioMovimientos(inventarioMovimientosRequestDto);

             InventarioMovimientos movimientoGuardado = await _repository.AgregarRegistroAsync(movimiento);

             InventarioMovimientosResponseDto movimientoDto = ManualMapperInventarioMovimientos.ToInventarioMovimientoResponse(movimientoGuardado);

             _logger.LogInformation("Movimento guardado: {}", movimientoDto);
             return CreatedAtAction(nameof(ObtenerInventarioMovimientoPorId), new { id = movimientoDto.Id },
                 movimientoDto);
         }
         catch (Exception ex)
         {
             _logger.LogError(ex, "Ocurrio un error al guardar el movimiento");
             return StatusCode(500, "Internal server error");
         }
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> ActualizarMovimiento(Guid id,
        [FromBody] InventarioMovimientosRequestDto inventarioMovimientosRequestDto)
    {
        _logger.LogInformation("Actualizando movimiento: {}", id);
        try
        {
            InventarioMovimientos? movimiento = await _repository.ObtenerPorIdAsync(id);
            if (movimiento == null)
            {
                _logger.LogWarning("El movimiento con el Id: {} no existe", id);
                return NotFound(new { error = "El movimiento no existe" });
            }

            ManualMapperInventarioMovimientos.ToUpdateInventarioMovimiento(movimiento, inventarioMovimientosRequestDto);

            bool movimientoActualizado = await _repository.ActualizarRegistroAsync(movimiento);
            if (!movimientoActualizado)
            {
                _logger.LogWarning("El movimiento no se actualizo.");
                return NoContent();
            }

            _logger.LogInformation("Movimineto {} actualizado.", movimiento.Id);
            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ocurrio un error al actualizar movimiento.");
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> EliminarMovimiento(Guid id)
    {
        _logger.LogInformation("Eliminando movimiento con el Id: {}", id);
        try
        {
            InventarioMovimientos? movimientoAEliminar = await _repository.ObtenerPorIdAsync(id);

            if (movimientoAEliminar == null)
            {
                _logger.LogWarning("El movimiento {Id} no existe.", id);
                return NotFound(new { error = "No existe el movimiento." });
            }

            bool eliminado = await _repository.EliminarRegistroAsync(movimientoAEliminar);
            if (!eliminado)
            {
                return Ok("Movimiento no eliminado.");
            }

            _logger.LogInformation("El movimiento {Id} ha sido eliminado.", id);
            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ocurrio un error al eliminar el movimiento.");
            return StatusCode(500, "Internal server error");
        }
    }
}*/