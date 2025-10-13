using AutoMapper;
using Inventarios.DTOs.DTO.Categoria;
using Inventarios.Entities.Models;

namespace Inventarios.Mapper;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<CategoriaToCreateDto, Categorias>();
        CreateMap<CategoriaToUpdateDto, Categorias>();
        CreateMap<Categorias, CategoriaToListDto>();
    }
}