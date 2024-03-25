namespace List.UniqueList.Exceptions.Tests;

public class UniqueListExceptionsTests
{
    UniqueList<int> uniqueList;

    [SetUp]
    public void SetUp()
    {
        uniqueList = new ();
        for (int i = 0; i < 10; ++i)
        {
            uniqueList.Insert(i, i);
        }
    }

    [Test]
    public void Insert_DuplicatedElement_ThrowsElementAlreadyExistsException()
    {
        for (int i = 0; i < 10; ++i)
        {
            Assert.Throws<ElementAlreadyExistsException>(() => uniqueList.Insert(0, i));
        }
    }

    [Test]
    public void Set_DuplicatedElement_ThrowsElementAlreadyExistsException()
    {
        Assert.Throws<ElementAlreadyExistsException>(() => uniqueList[0] = 1);
        Assert.Throws<ElementAlreadyExistsException>(() => uniqueList[9] = 1);
    }

    [Test]
    public void Remove_ElementNotInUniqueList_ThrowsElementNotFoundException()
    {
        Assert.Throws<ElementNotFoundException>(() => uniqueList.Remove(100));
        Assert.Throws<ElementNotFoundException>(() => uniqueList.Remove(-1));
        uniqueList.Remove(1);
        Assert.Throws<ElementNotFoundException>(() => uniqueList.Remove(1));
    }

}