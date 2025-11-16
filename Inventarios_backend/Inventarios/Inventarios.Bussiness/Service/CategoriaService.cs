using Inventarios.Bussiness.Interface.Repository;
using Inventarios.Bussiness.Mappers;
using Inventarios.DTOs.DTO.Request;
using Inventarios.DTOs.DTO.Response;
using Inventarios.Entities.Models;
using Microsoft.Extensions.Logging;

namespace Inventarios.Bussiness.Service;

public class CategoriaService : ICategoriaService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<CategoriaService> _logger;

    public CategoriaService(IUnitOfWork unitOfWork, ILogger<CategoriaService> logger)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _logger = logger;
    }

    public async Task<IEnumerable<CategoriaResponseDto>> ObtenerCategorias()
    {
        IEnumerable<Categorias> categorias = await _unitOfWork.CategoriasRepository.ObtenerTodosAsync();
        return ManualMapperCategorias.ToCategoriasResponse(categorias);
    }

    public async Task<CategoriaResponseDto> ObtenerCategoriaPorId(int id)
    {
        _logger.LogInformation("Intentando obtener categoria {Id}", id);
        
        Categorias? categoria = await _unitOfWork.CategoriasRepository.ObtenerPorIdAsync(id);

        if (categoria == null)
        {
            _logger.LogWarning("Categoria no encontrada: {Id}", id);
            throw new Exception("Categoria no encontrada");
        }
        
        return ManualMapperCategorias.ToCategoriasResponse(categoria);
    }

    public async Task<CategoriaResponseDto> AgregarCategoria(CategoriaRequestDto categoriaRequestDto)
    {
        _logger.LogInformation("Creando categoria {Nombre}", categoriaRequestDto.Nombre);

        Categorias categoria = ManualMapperCategorias.ToCategorias(categoriaRequestDto);

        await _unitOfWork.CategoriasRepository.AgregarRegistro(categoria);

        await _unitOfWork.SaveChangesAsync();
        
        _logger.LogInformation("Categoria {Nombre} agregado", categoriaRequestDto.Nombre);
        return ManualMapperCategorias.ToCategoriasResponse(categoria);
    }

    public async Task ActualizarCategoria(int id, CategoriaRequestDto categoriaRequestDto)
    {
        _logger.LogInformation("Intentando actualizar categoria {Id}", id);
        Categorias? categoria = await _unitOfWork.CategoriasRepository.ObtenerPorIdAsync(id);
        if (categoria == null)
        {
            _logger.LogWarning("Categoria no encontrada: {Id}", id);
            throw new Exception("Categoria no encontrada");
        } 
        
        ManualMapperCategorias.ToUpdateCategoria(categoria, categoriaRequestDto);
        categoria.UpdatedAt = DateTime.UtcNow;

        await _unitOfWork.SaveChangesAsync();
        _logger.LogInformation("Categoria actualizada {Id}", id);
    }

    public async Task EliminarCategoria(int id)
    {
        _logger.LogInformation("Intentando eliminar categoria {Id}", id);
        Categorias? categoria = await _unitOfWork.CategoriasRepository.ObtenerPorIdAsync(id);
        if (categoria == null)
        {
            _logger.LogWarning("Categoria no encontrada: {Id}", id);
            throw new Exception("Categoria no encontrada");
        }
        
        _unitOfWork.CategoriasRepository.EliminarRegistro(categoria);
        await _unitOfWork.SaveChangesAsync();
        
        _logger.LogInformation("Categoria eliminada {Id}", id);
    }
}