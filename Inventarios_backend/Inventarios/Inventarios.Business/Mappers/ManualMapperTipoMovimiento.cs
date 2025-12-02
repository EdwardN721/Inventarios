using Inventarios.DTOs.DTO.Request;
using Inventarios.DTOs.DTO.Response;
using Inventarios.Entities.Models;

namespace Inventarios.Business.Mappers;

public static class ManualMapperTipoMovimiento
{
    public static TipoMovimiento ToEntity(this TipoMovimientoRequestDto dto)
    {
        return new TipoMovimiento
        {
            Nombre =  dto.Nombre,
            Descripcion = dto.Descripcion,
            EsEntrada = dto.EsEntrada,
            EsSalida = dto.EsSalida,
            EsTransferenciaInterna = dto.EsTransferenciaInterna,
        };
    }

    public static void ToUpdateEntity(this TipoMovimientoRequestDto dto, TipoMovimiento entity)
    {
        entity.Nombre = dto.Nombre;
        entity.Descripcion = dto.Descripcion;
        entity.EsEntrada = dto.EsEntrada;
        entity.EsSalida = dto.EsSalida;
        entity.EsTransferenciaInterna = dto.EsTransferenciaInterna;
        entity.UpdatedAt = DateTime.UtcNow;
    }

    public static TipoMovimientoResponseDto ToDto(this TipoMovimiento entity)
    {
        return new TipoMovimientoResponseDto(
            entity.Id,
            entity.Nombre,
            entity.Descripcion,
            entity.EsEntrada,
            entity.EsSalida,
            entity.EsTransferenciaInterna,
            entity.CreatedAt,
            entity.UpdatedAt
        );
    }
}