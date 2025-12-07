using Inventarios.Entities.Models;
using Inventarios.DTOs.DTO.Request;
using Inventarios.DTOs.DTO.Response;

namespace Inventarios.Business.Interface.Services;

public interface IProveedorService
{
    Task<IEnumerable<ProveedorResponseDto>> ObtenerProveedores();
    Task<ProveedorResponseDto> ObtenerProveedorPorId(Guid id);
    Task<ProveedorResponseDto> GuardarProveedor(ProveedorRequestDto proveedor);
    Task<bool> ActualizarProveedor(Guid id, ProveedorRequestDto proveedor);
    Task<bool> EliminarProveedor(Guid id);
}