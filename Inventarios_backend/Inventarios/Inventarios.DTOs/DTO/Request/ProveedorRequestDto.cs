namespace Inventarios.DTOs.DTO.Request;

public record ProveedorRequestDto(
   string Nombre, 
   string PersonaContacto,
   string Telefono,
   string Correo,
   string Direccion
   );