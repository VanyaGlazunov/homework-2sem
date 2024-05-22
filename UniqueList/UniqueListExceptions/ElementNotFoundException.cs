namespace List.UniqueList.Exceptions;

/// <summary>
/// The exception that is thrown when the element is not found in the collection.
/// </summary>
public class ElementNotFoundException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ElementNotFoundException"/> class.
    /// </summary>
    /// <param name="message">Text that describes exception.</param>
    public ElementNotFoundException(string? message)
     : base(message)
    {
    }
}