namespace Inventarios.DTOs.DTO.Response;

public record ProveedorResponseDto(
    Guid Id,
    string Nombre, 
    string PersonaContacto,
    string Telefono,
    string Correo,
    string Direccion,
    DateTime CreatedAt,
    DateTime? UpdatedAt
);