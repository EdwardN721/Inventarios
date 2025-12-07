using FluentValidation.Results;

namespace Inventarios.Business.Exceptions;

/// <summary>
/// Excepcion que se lanza cuando una validacion de FlientValidation falla.
/// (resulta en un HTTP 400).
/// </summary>
public class ValidationException : Exception
{
    public IDictionary<string, string[]> Errors { get; }

    public ValidationException() : base("Una o más validaciones fallaron.")
    {
        Errors = new Dictionary<string, string[]>();
    }

    public ValidationException(IEnumerable<ValidationFailure> failures) : this()
    {
        Errors = failures
            .GroupBy(e => e.PropertyName, e=> e.ErrorMessage)
            .ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.ToArray());
    }
}