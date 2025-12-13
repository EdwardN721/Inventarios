using Inventarios.DTOs.DTO.Request;
using Inventarios.DTOs.DTO.Response;
using Inventarios.Entities.Models;

namespace Inventarios.Business.Mappers;

public static class ManualMapperInventarioStock
{
    /// <summary>
    /// Crea un inventario apartir de su objeto de transferencia de datos
    /// </summary>
    /// <param name="inventarioStockRequestDto">Dto de inventarioStock</param>
    /// <returns>Una entidad</returns>
    public static InventarioStock ToEntity(this InventarioStockRequestDto inventarioStockRequestDto)
    {
        return new InventarioStock()
        {
            Cantidad = inventarioStockRequestDto.Cantidad,
            Ubicacion = inventarioStockRequestDto.Ubicacion,
            ProductoId = inventarioStockRequestDto.ProductoId
        };
    }

    /// <summary>
    /// Actualiza un objeto de datos apartir de su DTO
    /// </summary>
    /// <param name="inventarioStockExistente">Objeto de la base de datos</param>
    /// <param name="inventarioStockRequestDto">Objeto que tranferira su informacion</param>
    public static void ToUpdateEntity(this InventarioStockRequestDto inventarioStockRequestDto, 
        InventarioStock inventarioStockExistente)
    {
        inventarioStockExistente.Cantidad = inventarioStockRequestDto.Cantidad;
        inventarioStockExistente.Ubicacion = inventarioStockRequestDto.Ubicacion;
        inventarioStockExistente.ProductoId = inventarioStockRequestDto.ProductoId;
        inventarioStockExistente.UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Mostrar un objeto en base de datos
    /// </summary>
    /// <param name="inventarioStock">Objeto con valor de la base de datos</param>
    /// <returns>Una entidad para el usuario final</returns>
    public static InventarioStockResponseDto ToDto(this InventarioStock inventarioStock)
    {
        return new InventarioStockResponseDto(
            inventarioStock.Id,
            inventarioStock.Cantidad,
            inventarioStock.Ubicacion,
            inventarioStock.CreatedAt,
            inventarioStock.UpdatedAt,
            inventarioStock.ProductoId);
    }

    /// <summary>
    /// Muestra una lista de la entidad en la base de datos 
    /// </summary>
    /// <param name="inventarioStocks">Lista de objetos con valor de la base de datos</param>
    /// <returns>Una lista de entidades a mostrar al usuario final</returns>
    public static IEnumerable<InventarioStockResponseDto> ToDto(this
        IEnumerable<InventarioStock>? inventarioStocks)
    {
        if (inventarioStocks == null)
        {
            return [];
            
        }
        return inventarioStocks.Select(ToDto);
    }
}