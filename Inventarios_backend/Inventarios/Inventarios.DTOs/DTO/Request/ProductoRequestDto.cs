namespace Inventarios.DTOs.DTO.Request;

public record ProductoRequestDto(
    string Nombre, 
    string? Descripcion,
    decimal Precio,
    string CodigoBarras,
    int CategoriaId,
    Guid ProveedorId
    );