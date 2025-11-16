using Inventarios.DTOs.DTO.Request;

namespace Inventarios.DTOs.DTO.Response;

public record CategoriaResponseDto : CategoriaRequestDto
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; } 
    public DateTime? UpdatedAt { get; set; }
};