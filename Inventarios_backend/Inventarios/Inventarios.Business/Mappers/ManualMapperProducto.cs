using Inventarios.DTOs.DTO.Request;
using Inventarios.DTOs.DTO.Response;
using Inventarios.Entities.Models;

namespace Inventarios.Business.Mappers;

public static class ManualMapperProducto
{
    /// <summary>
    /// Tranforma de un Dto a una entidad de producto
    /// </summary>
    /// <param name="requestDto">Dto de producto</param>
    /// <returns>Entidad producto</returns>
    public static Producto ToEntity(this ProductoRequestDto requestDto)
    {
        return new Producto
        {
            Nombre = requestDto.Nombre,
            Descripcion = requestDto.Descripcion,
            Precio = requestDto.Precio,
            CodigoBarras = requestDto.CodigoBarras,
        };
    }

    /// <summary>
    /// Actualiza una entidad de producto existente en la base de datos
    /// </summary>
    /// <param name="productoRequestDto">Dto del producto a actualizar</param>
    /// <param name="productoExistente">Entidad existente</param>
    public static void ToUpdateEntity(this ProductoRequestDto productoRequestDto, Producto productoExistente)
    {
        productoExistente.Nombre = productoRequestDto.Nombre;
        productoExistente.Descripcion = productoRequestDto.Descripcion;
        productoExistente.Precio = productoRequestDto.Precio;
        productoExistente.CodigoBarras = productoRequestDto.CodigoBarras;
        productoExistente.UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Entidad para mostrar al usuario 
    /// </summary>
    /// <param name="producto">Entidad producto existente en la base de datos</param>
    /// <returns>Entidad para mostrar al usuario</returns>
    public static ProductoResponseDto ToDto(this Producto producto)
    {
        return new ProductoResponseDto
        (
            producto.Id,
            producto.Nombre,
            producto.Descripcion,
            producto.Precio,
            producto.CodigoBarras,
            producto.CreatedAt,
            producto.UpdatedAt
        );
    }

    /// <summary>
    /// Lista de entidades producto para mostrar al usuario
    /// </summary>
    /// <param name="productos">Lista de entidades existentes en la base de datos</param>
    /// <returns>Lista de productos.</returns>
    public static IEnumerable<ProductoResponseDto> ToEntity(this IEnumerable<Producto>? productos)
    {
        if (productos == null)
        {
            return [];
            
        }
        return productos.Select(ToDto);
    }
}