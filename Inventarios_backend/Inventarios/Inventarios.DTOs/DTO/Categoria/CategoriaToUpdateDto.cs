namespace Inventarios.DTOs.DTO.Categoria;

public record CategoriaToUpdateDto(
    int Id, 
    string Nombre, 
    string? Descripcion
    );