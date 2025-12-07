namespace Inventarios.DTOs.DTO.Request;

public record TipoMovimientoRequestDto(
    string Nombre,
    string? Descripcion,
    bool EsEntrada,
    bool EsSalida,
    bool EsTransferenciaInterna
    );