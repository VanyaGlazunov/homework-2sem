namespace StackCalculator;

public interface IStack
{
    bool IsEmpty {get; }

    float Pop();

    void Push(float element);
}
