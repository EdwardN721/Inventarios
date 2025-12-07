using FluentValidation;
using Inventarios.DTOs.DTO.Request;

namespace Inventarios.Business.Validation;

public class ProveedorRequestDtoValidator : AbstractValidator<ProveedorRequestDto>
{
    public ProveedorRequestDtoValidator()
    {
        RuleFor(request => request.Nombre)
            .NotEmpty().WithMessage("Nombre del proveedor no puede estar vacio")
            .MaximumLength(50).WithMessage("No puede ser mayor a 50 caracteres");
        
        RuleFor(request => request.PersonaContacto)
            .NotEmpty().WithMessage("PersonaContacto no puede estar vacio")
            .MaximumLength(100).WithMessage("No puede ser mayor a 100 caracteres");
        
        RuleFor(request => request.Telefono)
            .NotEmpty().WithMessage("Telefono no puede estar vacio")
            .MaximumLength(10).WithMessage("No puede ser mayor a 10 caracteres");
        
        RuleFor(request => request.Direccion)
            .NotEmpty().WithMessage("Direccion no puede estar vacio")
            .MaximumLength(250).WithMessage("No puede ser mayor a 250 caracteres");
    }
}