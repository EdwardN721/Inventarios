namespace Inventarios.DTOs.DTO.Request;

public record CategoriaRequestDto
{
    public string Nombre { get; set; } = string.Empty;
    public string? Descripcion { get; set; } = string.Empty;
};