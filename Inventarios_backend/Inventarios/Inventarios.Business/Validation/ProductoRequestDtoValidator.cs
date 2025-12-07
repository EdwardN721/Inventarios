using FluentValidation;
using Inventarios.DTOs.DTO.Request;

namespace Inventarios.Business.Validation;

public class ProductoRequestDtoValidator : AbstractValidator<ProductoRequestDto>
{
    public ProductoRequestDtoValidator()
    {
        RuleFor(request => request.Nombre)
            .NotEmpty().WithMessage("El nombre no puede estar vacío")
            .MaximumLength(100).WithMessage("El nombre no puede ser mayor a 100 caracteres")
            .Matches("^[a-zA-Z0-9 áéíóúÁÉÍÓÚñÑ-]+$")
            .WithMessage("El nombre solo puede contener letras, números, espacios y guiones");

        RuleFor(request => request.Descripcion)
            .MaximumLength(250).WithMessage("La descripción no puede ser mayor a 250 caracteres");

        RuleFor(request => request.Precio)
            .NotEmpty().WithMessage("El precio no puede estar vacío")
            .GreaterThan(0).WithMessage("El precio debe ser mayor que 0")
            .PrecisionScale(10, 2, false)
            .WithMessage("El precio debe tener máximo 10 dígitos en total y hasta 2 decimales");


        RuleFor(request => request.CodigoBarras)
            .NotEmpty().WithMessage("El código de barras no puede estar vacío")
            .MaximumLength(50).WithMessage("El código de barras no puede ser mayor a 50 caracteres")
            .Matches("^[0-9]+$").WithMessage("El código de barras solo puede contener números");
    }
}
