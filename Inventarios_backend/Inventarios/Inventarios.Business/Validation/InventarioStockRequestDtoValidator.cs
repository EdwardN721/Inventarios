using FluentValidation;
using Inventarios.DTOs.DTO.Request;

namespace Inventarios.Business.Validation;

public class InventarioStockRequestDtoValidator : AbstractValidator<InventarioStockRequestDto>
{
    public InventarioStockRequestDtoValidator()
    {
        RuleFor(request => request.Cantidad)
            .GreaterThan(0).WithMessage("La cantidad no puede ser menor a 0");

        RuleFor(request => request.Ubicacion)
            .MinimumLength(5).WithMessage("La cantidad no puede ser menor a 5");
    }
}