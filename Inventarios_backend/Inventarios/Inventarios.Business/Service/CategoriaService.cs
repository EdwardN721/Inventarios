using Inventarios.Entities.Models;
using Inventarios.Business.Mappers;
using Inventarios.DTOs.DTO.Request;
using Microsoft.Extensions.Logging;
using Inventarios.DTOs.DTO.Response;
using Inventarios.Business.Exceptions;
using Inventarios.Business.Interface.Services;
using Inventarios.Business.Interface.Repository;

namespace Inventarios.Business.Service;

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
        IEnumerable<Categoria> categorias = await _unitOfWork.CategoriasRepository.ObtenerTodosAsync();
        return categorias.ToDto();
    }

    public async Task<CategoriaResponseDto> ObtenerCategoriaPorId(int id)
    {
        _logger.LogInformation("Intentando obtener categoria {Id}", id);
        
        Categoria? categoria = await _unitOfWork.CategoriasRepository.ObtenerPorIdAsync(id);

        if (categoria == null)
        {
            _logger.LogWarning("Categoria no encontrada: {Id}", id);
            throw new NotFoundException("Categoria no encontrada");
        }
        
        return categoria.ToDto();
    }

    public async Task<CategoriaResponseDto> AgregarCategoria(CategoriaRequestDto categoriaRequestDto)
    {
        _logger.LogInformation("Creando categoria {Nombre}", categoriaRequestDto.Nombre);

        // Categoria categoria = ManualMapperCategorias.ToEntity(categoriaRequestDto);
        Categoria categoria = categoriaRequestDto.ToEntity();

        await _unitOfWork.CategoriasRepository.AgregarRegistro(categoria);

        await _unitOfWork.SaveChangesAsync();
        
        _logger.LogInformation("Categoria {Nombre} agregado", categoriaRequestDto.Nombre);
        return categoria.ToDto();
    }

    public async Task ActualizarCategoria(int id, CategoriaRequestDto categoriaRequestDto)
    {
        _logger.LogInformation("Intentando actualizar categoria {Id}", id);
        Categoria? categoria = await _unitOfWork.CategoriasRepository.ObtenerPorIdAsync(id);
        if (categoria == null)
        {
            _logger.LogWarning("Categoria no encontrada: {Id}", id);
            throw new Exception("Categoria no encontrada");
        } 
        
        categoriaRequestDto.ToUpdateEntity(categoria);

        await _unitOfWork.SaveChangesAsync();
        _logger.LogInformation("Categoria actualizada {Id}", id);
    }

    public async Task EliminarCategoria(int id)
    {
        _logger.LogInformation("Intentando eliminar categoria {Id}", id);
        Categoria? categoria = await _unitOfWork.CategoriasRepository.ObtenerPorIdAsync(id);
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