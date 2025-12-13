namespace Inventarios.Extensions;

/// <summary>
/// Representa una excepción de negocio utilizada para controlar reglas,
/// validaciones y condiciones que no cumplen la lógica de dominio.
/// </summary>
public class BusinessExcepion : Exception
{
   
    /// <summary>
    /// Crea una nueva instancia de la excepción con un mensaje descriptivo.
    /// </summary>
    /// <param name="message">Mensaje que describe el error de negocio.</param>
    public BusinessExcepion(string? message) 
        : base(message)
    {
    }

    /// <summary>
    /// Crea una nueva instancia de la excepción con un mensaje descriptivo
    /// y una excepción interna que originó el error.
    /// </summary>
    /// <param name="message">Mensaje que describe el error de negocio.</param>
    /// <param name="innerException">Excepción interna que causó el error.</param>
    public BusinessExcepion(string? message, Exception? innerException) 
        : base(message, innerException)
    {
    }
}
