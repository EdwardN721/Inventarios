using Inventarios.Business.Exceptions;
using Inventarios.Business.Interface.Services;
using Inventarios.Business.Mappers;
using Inventarios.Bussiness.Interface.Repository;
using Inventarios.DTOs.DTO.Request;
using Inventarios.DTOs.DTO.Response;
using Inventarios.Entities.Models;
using Microsoft.Extensions.Logging;

namespace Inventarios.Business.Service;

public class ProveedorService : IProveedorService
{
    private readonly ILogger<ProveedorService> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public ProveedorService(ILogger<ProveedorService> logger, IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<ProveedorResponseDto>> ObtenerProveedores()
    {
        IEnumerable<Proveedor> proveedores = await _unitOfWork.ProveedorRepository.ObtenerTodosAsync();
        
        _logger.LogInformation("Proveedores encontrados: {}", proveedores.Count());
        return proveedores.ToDto();
    }

    public async Task<ProveedorResponseDto> ObtenerProveedorPorId(Guid id)
    {
        Proveedor proveedor = await _unitOfWork.ProveedorRepository.ObtenerPorIdAsync(id) 
                              ?? throw new NotFoundException($"Proveedor no encontrado con el Id: {id}");
        
        _logger.LogInformation("Proveedor encontrado: {}", proveedor);
        return proveedor.ToDto();
    }

    public async Task<ProveedorResponseDto> GuardarProveedor(ProveedorRequestDto proveedorDto)
    {
        Proveedor proveedor = proveedorDto.ToEntity();
        
        await _unitOfWork.ProveedorRepository.AgregarRegistro(proveedor);
        await _unitOfWork.SaveChangesAsync();

        _logger.LogInformation("Proveedor agregado: {}", proveedor.Nombre);
        return proveedor.ToDto();
    }

    public async Task<bool> ActualizarProveedor(Guid id, ProveedorRequestDto proveedorDto)
    {
        Proveedor proveedor = await _unitOfWork.ProveedorRepository.ObtenerPorIdAsync(id) 
                              ?? throw new NotFoundException($"Proveedor no encontrado con el Id: {id}");
        
        proveedorDto.ToUpdateEntity(proveedor);
        
        _unitOfWork.ProveedorRepository.ActualizarRegistro(proveedor);
        int actualizado = await _unitOfWork.SaveChangesAsync();
        
        _logger.LogInformation("Proveedor actualizado: {}", proveedor.Id);
        return actualizado != 0;
    }

    public async Task<bool> EliminarProveedor(Guid id)
    {
        Proveedor proveedor = await _unitOfWork.ProveedorRepository.ObtenerPorIdAsync(id) 
                              ?? throw new NotFoundException($"Proveedor no encontrado con el Id: {id}");
        
        _unitOfWork.ProveedorRepository.EliminarRegistro(proveedor);
        int eliminado = await _unitOfWork.SaveChangesAsync();
        
        _logger.LogInformation("Proveedor eliminado: {}", proveedor.Id);
        return eliminado != 0;
    }
}