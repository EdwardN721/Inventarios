using Inventarios.DTOs.DTO.Request;
using Inventarios.DTOs.DTO.Response;

namespace Inventarios.Business.Interface.Services;

public interface IProductoService
{
    Task<IEnumerable<ProductoResponseDto>> ObtenerProductos();
    Task<ProductoResponseDto> ObtenerProductoPorId(Guid id);
    Task<ProductoResponseDto> AgregarProducto(ProductoRequestDto dto);
    Task ActualizarProducto(Guid id, ProductoRequestDto dto);
    Task EliminarProducto(Guid id);
}