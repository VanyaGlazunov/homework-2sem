namespace List.UniqueList.Exceptions;

/// <summary>
/// The exception that is thrown when a method call causes occurance of duplicated elements in the collection with unique elemnts.
/// </summary>
public class ElementAlreadyExistsException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ElementAlreadyExistsException"/> class.
    /// </summary>
    /// <param name="message">Text that describes exception.</param>
    public ElementAlreadyExistsException(string? message)
     : base(message)
    {
    }
}
