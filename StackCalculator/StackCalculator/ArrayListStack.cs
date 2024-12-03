namespace StackCalculator;

/// <summary>
/// Stack class based on LinkedList.
/// </summary>
public class ArrayListStack : IStack
{
    private float[] stack = new float[1];
    private int size;
    private int capacity = 1;

    /// <inheritdoc/>
    public bool IsEmpty => this.size == 0;

    /// <inheritdoc/>
    public void Push(float element)
    {
        if (this.size + 1 > this.capacity)
        {
            Array.Resize(ref this.stack, 2 * this.capacity);
            this.capacity *= 2;
        }

        this.stack[this.size++] = element;
    }

    /// <inheritdoc/>
    public (float Element, bool IsPopped) Pop()
    {
        if (this.IsEmpty)
        {
            return (0, false);
        }

        return (this.stack[--this.size], true);
    }
}
