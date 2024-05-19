namespace Routers.Exceptions;

/// <summary>
/// Exception thrown when network representaion is incorrect.
/// </summary>
public class IncorrectNetworkFormatException : System.Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="IncorrectNetworkFormatException"/> class with a specified error message.
    /// </summary>
    /// <param name="message">Text of the error message.</param>
    public IncorrectNetworkFormatException(string message)
     : base(message)
    {
    }
}