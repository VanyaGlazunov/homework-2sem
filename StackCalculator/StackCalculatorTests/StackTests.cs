namespace StackCalculator.Tests;

public class StackTests
{
    ListStack listStack;
    LinkedListStack linkedListStack;
    
    [SetUp]
    public void SetUp()
    {
        listStack = new ();
        linkedListStack = new ();
    }

    [Test]
    public void Pop_EmptyStack_ReturnsFalse()
    {
        var (element, isPopped) = listStack.Pop();
        Assert.That(!isPopped);
        (element, isPopped) = linkedListStack.Pop();
        Assert.That(!isPopped);
    }

    [Test]
    public void Push_OneElement_IsNotEmpty()
    {
        listStack.Push(0); 
        linkedListStack.Push(0);
        Assert.That(!listStack.IsEmpty && !linkedListStack.IsEmpty);
    }

    [Test]
    public void PushPop_OneElement_IsEmpty()
    {
        listStack.Push(0);
        listStack.Pop();
        linkedListStack.Push(0);
        linkedListStack.Pop();
        Assert.That(listStack.IsEmpty && linkedListStack.IsEmpty);
    }

    [Test]
    public void PushPop_TenElements_PopReturnsExpectedValueAndTrue()
    {
        for (var i = 0; i < 10; ++i)
        {
            listStack.Push(i);
            linkedListStack.Push(i);
        }

        for (var i = 9; i >= 0; --i)
        {
            var (element, isPopped) = listStack.Pop();
            Assert.That(element == i && isPopped);
            (element, isPopped) = linkedListStack.Pop();
            Assert.That(element == i && isPopped);
        }
    }
}
