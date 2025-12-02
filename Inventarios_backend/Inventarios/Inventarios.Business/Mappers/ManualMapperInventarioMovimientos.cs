using Inventarios.DTOs.DTO.Request;
using Inventarios.DTOs.DTO.Response;
using Inventarios.Entities.Models;

namespace Inventarios.Business.Mappers;

public static class ManualMapperInventarioMovimientos
{
    /// <summary>
    /// Crea un Movimiento de inventario apartit de su DTO
    /// </summary>
    /// <param name="requestDto">DTO de InventarioMovimiento</param>
    /// <returns>Una entidad InventarioMovimientos</returns>
    public static InventarioMovimiento ToEntity(this InventarioMovimientosRequestDto requestDto)
    {
        return new InventarioMovimiento()
        {
            FechaMovimiento = requestDto.FechaMovimiento,
            Cantidad = requestDto.Cantidad
        };
    }

    /// <summary>
    /// Actualiza un objeto en base de datos apartir de su DTO
    /// </summary>
    /// <param name="inventarioMovimientoExistente">Objeto de la base de datos</param>
    /// <param name="requestDto">DTO de InventarioMovimientos</param>
    public static void ToUpdateEntity(this InventarioMovimientosRequestDto requestDto, 
        InventarioMovimiento inventarioMovimientoExistente)
    {
        inventarioMovimientoExistente.FechaMovimiento = requestDto.FechaMovimiento;
        inventarioMovimientoExistente.Cantidad = requestDto.Cantidad;
    }

    /// <summary>
    /// Muestra el objeto de la base de datos tranformado
    /// </summary>
    /// <param name="inventarioMovimientoExistente">Objeto se obtiene de la base de datos</param>
    /// <returns>DTO de inventario movimientos</returns>
    public static InventarioMovimientosResponseDto ToDto(this
        InventarioMovimiento inventarioMovimientoExistente)
    {
        return new InventarioMovimientosResponseDto()
        {
            Id = inventarioMovimientoExistente.Id,
            FechaMovimiento = inventarioMovimientoExistente.FechaMovimiento,
            Cantidad = inventarioMovimientoExistente.Cantidad
        };
    }

    /// <summary>
    /// Devuelve una lista de Movimientos
    /// </summary>
    /// <param name="inventariosMovimientos">Lista de inventario Movmientos</param>
    /// <returns>Lista de movimientos para el usuario final</returns>
    public static IEnumerable<InventarioMovimientosResponseDto> ToDto(this
        IEnumerable<InventarioMovimiento>? inventariosMovimientos)
    {
        if (inventariosMovimientos == null)
        {
            return [];
            
        }
        return inventariosMovimientos.Select(ToDto);
    }
}