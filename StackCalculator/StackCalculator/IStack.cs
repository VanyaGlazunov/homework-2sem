namespace StackCalculator;

/// <summary>
/// Represents a first-in-last-out collection of floats.
/// </summary>
public interface IStack
{
    /// <summary>
    /// Gets a value indicating whether the stack is empty.
    /// </summary>
    bool IsEmpty { get; }

    /// <summary>
    /// Returns last element and erases it.
    /// </summary>
    /// <returns>Last element in the stack and bool flag indicating whether stack was not empty.</returns>
    (float Element, bool IsPopped) Pop();

    /// <summary>
    /// Adds element to the end of the collection.
    /// </summary>
    /// <param name="element">Element to add to the collection.</param>
    void Push(float element);
}
