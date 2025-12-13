using FluentValidation;
using Inventarios.DTOs.DTO.Request;
namespace Inventarios.Business.Validation;

public class InventarioMovimientoRequestDtoValidator 
    : AbstractValidator<InventarioMovimientosRequestDto>
{
    public InventarioMovimientoRequestDtoValidator()
    {
        // ✅ Fecha: no puede ser futura
        RuleFor(r => r.FechaMovimiento)
            .NotNull().WithMessage("La fecha es obligatoria.")
            .LessThanOrEqualTo(DateTime.Today)
            .WithMessage("La fecha no puede ser futura.");

        // ✅ Cantidad: debe ser mayor a 0
        RuleFor(r => r.Cantidad)
            .GreaterThan(0)
            .WithMessage("La cantidad debe ser mayor a cero.");
    }

}
