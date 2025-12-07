namespace Inventarios.DTOs.DTO.Response;

public record TipoMovimientoResponseDto(
    int Id,
    string Nombre,
    string? Descripcion,
    bool EsEntrada,
    bool EsSalida,
    bool EsTransferenciaInterna,
    DateTime CreatedAt,
    DateTime? UpdatedAt
    );