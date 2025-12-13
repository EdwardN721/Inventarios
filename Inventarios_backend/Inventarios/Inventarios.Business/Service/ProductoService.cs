using Inventarios.Entities.Models;
using Microsoft.Extensions.Logging;
using Inventarios.DTOs.DTO.Request;
using Inventarios.Business.Mappers;
using Inventarios.DTOs.DTO.Response;
using Inventarios.Business.Exceptions;
using Inventarios.Business.Interface.Services;
using Inventarios.Business.Interface.Repository;

namespace Inventarios.Business.Service;

public class ProductoService : IProductoService
{
    private readonly ILogger<ProductoService> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public ProductoService(ILogger<ProductoService> logger, IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<IEnumerable<ProductoResponseDto>> ObtenerProductos()
    {
        IEnumerable<Producto> productos = await _unitOfWork.ProductosRepository.ObtenerTodosAsync();
        
        _logger.LogInformation("Productos obtenidos: {Productos}", productos.Count());
        return productos.ToDto();
    }

    public async Task<ProductoResponseDto> ObtenerProductoPorId(Guid id)
    {
        Producto producto = await _unitOfWork.ProductosRepository.ObtenerPorIdAsync(id) 
                            ?? throw new NotFoundException("Producto con el Id no existe.");
        
        _logger.LogInformation("Producto obtenido Id: {Id}", producto.Id);
        return producto.ToDto(); 
    }

    public async Task<ProductoResponseDto> AgregarProducto(ProductoRequestDto dto)
    {
        Producto producto = dto.ToEntity();
        await _unitOfWork.ProductosRepository.AgregarRegistro(producto);
        await _unitOfWork.SaveChangesAsync();
        
        _logger.LogInformation("Producto agregado: {Producto}", producto.Id);
        return producto.ToDto();
    }

    public async Task ActualizarProducto(Guid id, ProductoRequestDto dto)
    {
        Producto producto = await _unitOfWork.ProductosRepository.ObtenerPorIdAsync(id) 
                            ?? throw new NotFoundException("Producto con el Id no existe.");
        
        dto.ToUpdateEntity(producto);
        await _unitOfWork.SaveChangesAsync();
        
        _logger.LogInformation("Producto actualizado: {Producto}", producto.Id);
    }

    public async Task EliminarProducto(Guid id)
    {
        Producto producto = await _unitOfWork.ProductosRepository.ObtenerPorIdAsync(id) 
                            ?? throw new NotFoundException("Producto con el Id no existe.");
        _unitOfWork.ProductosRepository.EliminarRegistro(producto);
        await _unitOfWork.SaveChangesAsync();
        
        _logger.LogInformation("Producto eliminado: {Producto}", producto.Id);
    }
}