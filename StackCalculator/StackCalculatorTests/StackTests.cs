namespace StackCalculator.Tests;

public class StackTests
{
    private static IEnumerable<TestCaseData> Stack()
    {
        yield return new TestCaseData(new ListStack());
        yield return new TestCaseData(new LinkedListStack());
    }

    [TestCaseSource(nameof(Stack))]
    public void Pop_EmptyStack_ReturnsFalse(IStack stack)
    {
        var (element, isPopped) = stack.Pop();  
        Assert.That(!isPopped);
    }

    [TestCaseSource(nameof(Stack))]
    public void Push_OneElement_IsNotEmpty(IStack stack)
    {
        stack.Push(0); 
        stack.Push(0);
        Assert.That(!stack.IsEmpty);
    }

    [TestCaseSource(nameof(Stack))]
    public void PushPop_OneElement_IsEmpty(IStack stack)
    {
        stack.Push(0);
        stack.Pop();
        Assert.That(stack.IsEmpty);
    }

    [TestCaseSource(nameof(Stack))]
    public void PushPop_TenElements_PopReturnsExpectedValueAndTrue(IStack stack)
    {
        for (var i = 0; i < 10; ++i)
        {
            stack.Push(i);
        }

        for (var i = 9; i >= 0; --i)
        {
            var (element, isPopped) = stack.Pop();
            Assert.That(element == i && isPopped);
        }
    }
}
