using Inventarios.DTOs.DTO.Request;
using Inventarios.DTOs.DTO.Response;
using Inventarios.Entities.Models;

namespace Inventarios.Business.Mappers;

public static class ManualMapperProveedor
{
    /// <summary>
    /// Transforma un Dto a una entidad
    /// </summary>
    /// <param name="proveedor">Dto de proveedor</param>
    /// <returns>Una entidad de proveedor</returns>
    public static Proveedor ToEntity(this ProveedorRequestDto proveedor)
    {
        return new Proveedor
        {
            Nombre = proveedor.Nombre,
            PersonaContacto = proveedor.PersonaContacto,
            Telefono = proveedor.Telefono,
            Correo = proveedor.Correo,
            Direccion = proveedor.Direccion,
        };
    }

    /// <summary>
    /// Actualizar una entidad de proveedor existente 
    /// </summary>
    /// <param name="proveedor">Dto de proveedor</param>
    /// <param name="proveedorExistente">Proveedor existente de una base de datos</param>
    public static void ToUpdateEntity(this ProveedorRequestDto proveedor, Proveedor proveedorExistente)
    {
        proveedorExistente.Nombre = proveedor.Nombre;
        proveedorExistente.PersonaContacto = proveedor.PersonaContacto;
        proveedorExistente.Telefono = proveedor.Telefono;
        proveedorExistente.Correo = proveedor.Correo;
        proveedorExistente.Direccion = proveedor.Direccion;
        proveedorExistente.UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Entidad para mostrar al usuario
    /// </summary>
    /// <param name="proveedor">Proveedor existente en la base de datos</param>
    /// <returns>Dto de proveedor</returns>
    public static ProveedorResponseDto ToDto(this Proveedor proveedor)
    {
        return new ProveedorResponseDto(
            proveedor.Id,
            proveedor.Nombre,
            proveedor.PersonaContacto,
            proveedor.Telefono,
            proveedor.Correo,
            proveedor.Direccion,
            proveedor.CreatedAt,
            proveedor.UpdatedAt);
    }

    /// <summary>
    /// Lista de proveedores en base de datos 
    /// </summary>
    /// <param name="proveedores">Lista de proveedores existentes</param>
    /// <returns>Lista de Dto de proveedores</returns>
    public static IEnumerable<ProveedorResponseDto> ToDto(this IEnumerable<Proveedor>? proveedores)
    {
        if (proveedores == null)
        {
            return [];
        }

        return proveedores.Select(ToDto);
    }
}