using Inventarios.DTOs.DTO.Request;
using Inventarios.DTOs.DTO.Response;

namespace Inventarios.Bussiness.Service;

public interface ICategoriaService
{
    Task<IEnumerable<CategoriaResponseDto>> ObtenerCategorias();
    Task<CategoriaResponseDto> ObtenerCategoriaPorId(int id);
    Task<CategoriaResponseDto> AgregarCategoria(CategoriaRequestDto categoriaRequestDto);
    Task ActualizarCategoria(int id, CategoriaRequestDto categoriaRequestDto);
    Task EliminarCategoria(int id);
}