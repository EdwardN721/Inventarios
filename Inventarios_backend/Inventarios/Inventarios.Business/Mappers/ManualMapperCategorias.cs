using Inventarios.DTOs.DTO.Request;
using Inventarios.DTOs.DTO.Response;
using Inventarios.Entities.Models;

namespace Inventarios.Business.Mappers;

public static class ManualMapperCategorias
{
    /// <summary>
    /// Tranforma de un DTO a una entidad categoria
    /// </summary>
    /// <param name="categoria">Dto de categoria</param>
    /// <returns>Una entidad de catergoria</returns>
    public static Categoria ToEntity(this CategoriaRequestDto categoria)
    {
        return new Categoria
        {
            Nombre = categoria.Nombre,
            Descripcion = categoria.Descripcion,
            CreatedAt =  DateTime.UtcNow,
        };
    }

    /// <summary>
    /// Actualiza una entidad de categoria
    /// </summary>
    /// <param name="categoria">Dto de la entidad de categoria</param>
    /// <param name="entity">Entidad existente de la base de datos</param>
    public static void ToUpdateEntity(this CategoriaRequestDto categoria, Categoria entity)
    {
        entity.Nombre = categoria.Nombre;
        entity.Descripcion = categoria.Descripcion;
        entity.UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Muestra al usuario una entidad de categoria
    /// </summary>
    /// <param name="categoria">Entidad existente de base de datos</param>
    /// <returns>Un Dto de la entidad categoria</returns>
    public static CategoriaResponseDto ToDto(this Categoria categoria)
    {
        return new CategoriaResponseDto
        {
            Id = categoria.Id,
            Nombre = categoria.Nombre,
            Descripcion = categoria.Descripcion,
            CreatedAt = categoria.CreatedAt,
            UpdatedAt = categoria.UpdatedAt
        };
    }

    /// <summary>
    /// Muestra una lista al usuario de entidades de categoria
    /// </summary>
    /// <param name="categorias">Lista de categorias existentes en Base de datos</param>
    /// <returns>Lista de entidades</returns>
    public static IEnumerable<CategoriaResponseDto> ToDto(this IEnumerable<Categoria>? categorias)
    {
        if (categorias == null){ return [];}
        return categorias.Select(ToDto);
    }
}