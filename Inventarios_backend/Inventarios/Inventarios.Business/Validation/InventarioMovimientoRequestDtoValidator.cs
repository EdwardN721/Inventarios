using FluentValidation;
using Inventarios.DTOs.DTO.Request;

namespace Inventarios.Business.Validation;

public class InventarioMovimientoRequestDtoValidator : AbstractValidator<InventarioMovimientosRequestDto>
{
    public InventarioMovimientoRequestDtoValidator()
    {
        RuleFor(request => request.FechaMovimiento)
            .NotNull().WithMessage("No puede estar vacio.")
            .LessThan(DateTime.Today).WithMessage("No se puede elegir una fecha menor a la de hoy.");
        
        RuleFor(request => request.Cantidad)
            .NotNull().WithMessage("No puede estar vacio.")
            .GreaterThan(0).WithMessage("La cantidad no puede ser menor a 0");
        
        
        
    }
}