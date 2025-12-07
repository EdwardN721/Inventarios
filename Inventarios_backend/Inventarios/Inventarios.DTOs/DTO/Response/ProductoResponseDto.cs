namespace Inventarios.DTOs.DTO.Response;

public record ProductoResponseDto(
    Guid Id,  
    string Nombre, 
    string? Descripcion,
    decimal Precio,
    string CodigoBarras,
    DateTime CreatedAt,
    DateTime? UpdatedAt
    );