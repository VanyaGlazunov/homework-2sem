namespace StackCalculator;

public class ListStack : IStack
{
    private List<float> list;

    public ListStack() => this.list = new ();

    public bool IsEmpty { get => this.list.Count == 0; }

    public float Pop()
    {
        if (this.IsEmpty)
        {
            throw new InvalidOperationException("Cannot pop from empty stack");
        }

        var popped = this.list.Last();
        this.list.RemoveAt(this.list.Count - 1);
        return popped;
    }

    public void Push(float element) => this.list.Add(element);
}
