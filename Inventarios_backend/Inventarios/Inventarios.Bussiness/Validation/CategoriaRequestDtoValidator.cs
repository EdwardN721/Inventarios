using FluentValidation;
using Inventarios.DTOs.DTO.Request;

namespace Inventarios.Bussiness.Validation;

public class CategoriaRequestDtoValidator : AbstractValidator<CategoriaRequestDto>
{
    public CategoriaRequestDtoValidator()
    {
        RuleFor(request => request.Nombre)
            .NotEmpty().WithMessage("El campo Nombre es obligatorio")
            .MaximumLength(20).WithMessage("El nombre no puede ser mayor a 20 caracteres.")
            .MinimumLength(5).WithMessage("El nombre no puede ser menor a 5 caracteres.");
        
        RuleFor(request => request.Descripcion)
            .MaximumLength(20).WithMessage("El nombre no puede ser mayor a 50 caracteres.")
            .MinimumLength(5).WithMessage("El nombre no puede ser menor a 5 caracteres.");
    }
}