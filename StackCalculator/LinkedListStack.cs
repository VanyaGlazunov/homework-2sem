namespace StackCalculator;

/// <summary>
/// Stack class based on LinkedList.
/// </summary>
public class LinkedListStack : IStack
{
    private readonly LinkedList<float> linkedList;

    /// <summary>
    /// Initializes a new instance of the <see cref="LinkedListStack"/> class.
    /// </summary>
    public LinkedListStack() => this.linkedList = new ();

    /// <inheritdoc/>
    public bool IsEmpty { get => this.linkedList.Count == 0; }

    /// <inheritdoc/>
    public (float element, bool isPopped) Pop()
    {
        var popped = this.linkedList.Last;
        if (popped is null)
        {
            return (0, false);
        }

        this.linkedList.RemoveLast();
        return (popped.Value, true);
    }

    /// <inheritdoc/>
    public void Push(float element) => this.linkedList.AddLast(element);
}