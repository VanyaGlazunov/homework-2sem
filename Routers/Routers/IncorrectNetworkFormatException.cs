namespace Routers.Exceptions;

public class IncorrectNetworkFormatExceptionException : System.Exception
{
    public IncorrectNetworkFormatExceptionException(string message) : base(message) { }
    public IncorrectNetworkFormatExceptionException(string message, System.Exception inner) : base(message, inner) { }
}