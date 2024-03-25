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
