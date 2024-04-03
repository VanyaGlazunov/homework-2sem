namespace Routers.Exceptions;

/// <summary>
/// The exception that is thrown when graph should be connected but is not.
/// </summary>
public class GraphIsDisconnectedExpceptionException : System.Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GraphIsDisconnectedExpceptionException"/> class.
    /// </summary>
    public GraphIsDisconnectedExpceptionException()
    {
    }
}