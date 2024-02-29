namespace StackCalculator;

public class LinkedListStack : IStack
{
    private LinkedList<float> linkedList;

    public LinkedListStack() => this.linkedList = new ();

    public bool IsEmpty { get => this.linkedList.Count == 0; }

    public float Pop()
    {
        if (this.IsEmpty)
        {
            throw new InvalidOperationException("Cannot pop from empty stack");
        }

        var popped = this.linkedList.Last;
        this.linkedList.RemoveLast();
        return popped.Value;
    }

    public void Push(float element) => this.linkedList.AddLast(element);
}