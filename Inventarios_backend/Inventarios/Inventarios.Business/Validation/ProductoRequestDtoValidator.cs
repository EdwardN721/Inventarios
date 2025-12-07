using FluentValidation;
using Inventarios.DTOs.DTO.Request;

namespace Inventarios.Business.Validation;

public class ProductoRequestDtoValidator : AbstractValidator<ProductoRequestDto>
{
    public ProductoRequestDtoValidator()
    {
        RuleFor(request => request.Nombre)
            .NotEmpty().WithMessage("El nombre no puede estar vacio")
            .MaximumLength(100).WithMessage("La cantidad no puede ser mayor a 100 caracteres");
        
        RuleFor(request => request.Descripcion)
            .MaximumLength(250).WithMessage("La descripción no puede ser mayor a 250 caracteres");

        RuleFor(request => request.Precio)
            .NotEmpty().WithMessage("El precio no puede estar vacio")
            .PrecisionScale(6, 2, false).WithMessage("No precio debe ser menor a 6 caracteres y menos de 2 decimales");
        
        RuleFor(request => request.CodigoBarras)
            .NotEmpty().WithMessage("Código de barras no puede estar vacio")
            .MaximumLength(50).WithMessage("No puede ser mayor a 50 caracteres");
    }
}