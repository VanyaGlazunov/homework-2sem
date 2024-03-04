namespace StackCalculator;

/// <summary>
/// Stack class based on List.
/// </summary>
public class ListStack : IStack
{
    private readonly List<float> list;

    /// <summary>
    /// Initializes a new instance of the <see cref="ListStack"/> class.
    /// </summary>
    public ListStack() => this.list = new ();

    /// <inheritdoc/>
    public bool IsEmpty { get => this.list.Count == 0; }

    /// <inheritdoc/>
    public (float element, bool isPopped) Pop()
    {
        if (this.IsEmpty)
        {
            return (0, false);
        }

        var popped = this.list.Last();
        this.list.RemoveAt(this.list.Count - 1);
        return (popped, true);
    }

    /// <inheritdoc/>
    public void Push(float element) => this.list.Add(element);
}
