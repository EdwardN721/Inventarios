using Inventarios.DTOs.DTO.Request;
using Inventarios.DTOs.DTO.Response;
using Inventarios.Entities.Models;

namespace Inventarios.Bussiness.Mappers;

public static class ManualMapperCategorias
{
    /// <summary>
    /// Crear una categoria apartir de su objeto de transferencia de datos
    /// </summary>
    /// <param name="categoriaRequestDto">Objeto de transferencia de datos</param>
    /// <returns>Una entidad</returns>
    public static Categorias ToCategorias(CategoriaRequestDto categoriaRequestDto)
    {
        return new Categorias()
        {
            Nombre = categoriaRequestDto.Nombre,
            Descripcion = categoriaRequestDto.Descripcion,
        };
    }

    /// <summary>
    /// Actualizar un objeto en base de datos apartid de su transferencia de datos
    /// </summary>
    /// <param name="categoriaExistente">Objeto en base de datos</param>
    /// <param name="categoriaRequestDto">Objeto de transferencia de datos</param>
    public static void ToUpdateCategoria(Categorias categoriaExistente, CategoriaRequestDto categoriaRequestDto)
    {
        categoriaExistente.Nombre = categoriaRequestDto.Nombre;
        categoriaExistente.Descripcion = categoriaRequestDto.Descripcion;
    }

    /// <summary>
    /// Mostrar el objeto de base de datos
    /// </summary>
    /// <param name="categoria">Objeto de transefencia de datos para el usuario final</param>
    /// <returns>Objeto de base de datos</returns>
    public static CategoriaResponseDto ToCategoriasResponse(Categorias categoria)
    {
        return new CategoriaResponseDto()
        {
            Id = categoria.Id,
            Nombre = categoria.Nombre,
            Descripcion = categoria.Descripcion,
            CreatedAt = categoria.CreatedAt,
            UpdatedAt = categoria.UpdatedAt
        };
    }

    /// <summary>
    /// Devuelve una lista de categorias para mostrar al usuario final  
    /// </summary>
    /// <param name="categorias">Lista de categorias en la base de datos</param>
    /// <returns>Lista de categorias para mostrar al usuario final</returns>
    public static IEnumerable<CategoriaResponseDto> ToCategoriasResponse(IEnumerable<Categorias> categorias)
    {
        return categorias.Select(ToCategoriasResponse);
    }
}