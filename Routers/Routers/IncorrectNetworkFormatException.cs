namespace Routers.Exceptions;

/// <summary>
/// Exception thrown when network representaion is incorrect.
/// </summary>
public class IncorrectNetworkFormatExceptionException : System.Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="IncorrectNetworkFormatExceptionException"/> class with a specified error message.
    /// </summary>
    /// <param name="message">Text of the error message.</param>
    public IncorrectNetworkFormatExceptionException(string message)
     : base(message)
    {
    }
}