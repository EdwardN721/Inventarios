namespace Inventarios.DTOs.DTO.Categoria;

public record CategoriaToListDto(
    Guid Id,
    string Nombre,
    string? Descripcion,
    DateTime CreatedAt,
    DateTime? UpdatedAt
    );
