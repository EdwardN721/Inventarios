using FluentValidation;
using Inventarios.DTOs.DTO.Request;

namespace Inventarios.Business.Validation;

public class CategoriaRequestDtoValidator : AbstractValidator<CategoriaRequestDto>
{
    public CategoriaRequestDtoValidator()
    {
        RuleFor(request => request.Nombre)
            .NotEmpty().WithMessage("El campo Nombre es obligatorio")
            .MaximumLength(50).WithMessage("El nombre no puede ser mayor a 50 caracteres.")
            .MinimumLength(5).WithMessage("El nombre no puede ser menor a 5 caracteres.");
        
        RuleFor(request => request.Descripcion)
            .MaximumLength(250).WithMessage("El nombre no puede ser mayor a 250 caracteres.")
            .MinimumLength(5).WithMessage("El nombre no puede ser menor a 5 caracteres.");
    }
}