using FluentValidation;
using Inventarios.DTOs.DTO.Request;

namespace Inventarios.Business.Validation;

public class TipoMovimientoRequestDtoValidator : AbstractValidator<TipoMovimientoRequestDto>
{
    public TipoMovimientoRequestDtoValidator()
    {
        RuleFor(request => request.Nombre)
            .NotEmpty().WithMessage("La nombre no puede estar vacio")
            .MaximumLength(20).WithMessage("No puede ser mayor a 20 caracteres");
        
        RuleFor(request => request.Descripcion)
            .MaximumLength(100).WithMessage("No puede ser mayor a 100 caracteres");

        RuleFor(request => request.EsEntrada)
            .NotEmpty().WithMessage("Entrada no puede estar vacio");

        RuleFor(request => request.EsSalida)
            .NotEmpty().WithMessage("Salida no puede estar vacio");
        
        RuleFor(request => request.EsTransferenciaInterna)
            .NotEmpty().WithMessage("Transferencia no puede estar vacio");

    }
}