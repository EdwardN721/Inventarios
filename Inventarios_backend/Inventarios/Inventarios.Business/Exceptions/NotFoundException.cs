namespace Inventarios.Business.Exceptions;

/// <summary>
/// Excepcion que se lanza cuando no se encuentra un recurso
/// (resulta en un HTTP 404).
/// </summary>
public class NotFoundException : Exception
{
    public NotFoundException(string? message) : base(message)
    {
    }
}