namespace SkipList.Tests;

public class Tests
{

    [Test]
    public void Contains_OneElement_ReturnsExpectedResult()
    {
        var skipList = new SkipList<int>();
        skipList.Add(1);
        Assert.That(skipList.Contains(1), Is.True);
        Assert.That(skipList.Contains(2), Is.False);
    }

    [Test]
    public void Constructor_FromListOfIntsWithDefaultComparer_ContainsEveryElemFromList()
    {
        var list = new List<int> { 1, 2, 1000, 100, -1, 0 };
        var skipList = new SkipList<int>(list); 
        Assert.That(skipList.Count, Is.EqualTo(6));
        foreach (var elem in list)
        {
            Assert.That(skipList.Contains(elem), Is.True);
        }
    }

    [Test]
    public void Add_IntsToEmptySKipList_ContainsEveryAddedElement()
    {
        var list = new List<int> { 1, 2, 1000, 100, -1, 0 };
        var skipList = new SkipList<int> ();
        foreach (var elem in list)
        {
            skipList.Add(elem);
        }

        Assert.That(skipList.Count, Is.EqualTo(6));
        
        foreach (var elem in list)
        {
            Assert.That(skipList.Contains(elem), Is.True, $"elem is {elem}");
        }
    }

    [Test]
    public void Remove_OneElement_DoesNotContainRemovedElement()
    {
        var skipList = new SkipList<int>([1, 2, 1000, 100, -1, 0]); 
        skipList.Remove(0);
        Assert.That(skipList.Contains(0), Is.False);
    }

    [Test]
    public void Clear_NotEmptySkipList_DoesNotContainElements()
    {
        var list = new List<int> { 1, 2, 1000, 100, -1, 0 };
        var skipList = new SkipList<int> (list);
        skipList.Clear();
        foreach (var elem in list)
        {
            Assert.That(skipList.Contains(elem), Is.False);
        }
    }

    [Test]
    public void IndexOf_ElementInSkipList_ReturnsExpectedValue()
    {
        var skipList = new SkipList<int>([1, 2, 1000, 100, -1, 0]); 
        Assert.That(skipList.IndexOf(-1), Is.EqualTo(0), $"Contain? {skipList.Contains(-1)}");
        Assert.That(skipList.IndexOf(0), Is.EqualTo(1));
        Assert.That(skipList.IndexOf(1000), Is.EqualTo(5));
    }

    [Test]
    public void CopyTo_ArrayHasEnoughSpace_ArrayContainsEveryCopiedElement()
    {
        var list = new List<int> { 1, 2, 1000, 100, -1, 0 };
        list.Sort();
        var skipList = new SkipList<int> (list);
        var array = new int[6];
        skipList.CopyTo(array, 0);
        for (int i = 0; i < 6; ++i)
        {
            Assert.That(array[i], Is.EqualTo(list[i]));
        }
    }

    [Test]
    public void Enumerator_TryUsingAreEqual_ShouldPassTest()
    {
        var list = new List<int> { 1, 2, 1000, 100, -1, 0 };
        var skipList = new SkipList<int>(list);
        list.Sort();
        CollectionAssert.AreEqual(skipList, list);
    }

    [Test]
    public void Enumerator_InsideForeach()
    {
        var list = new List<int> { 1, 2, 1000, 100, -1, 0 };
        var skipList = new SkipList<int>(list);
        var actual = new List<int>();
        foreach (var item in skipList)
        {
            actual.Add(item);
        }
        list.Sort();
        CollectionAssert.AreEqual(actual, list);
    }

    [Test]
    public void Enumerator_ChangeCollectionAfterCreatingEnumerator_ThrowsInvalidOperationException()
    {
        var skipList = new SkipList<int>([1, 2, 1000, 100, -1, 0]); 
        var enumerator = skipList.GetEnumerator();
        enumerator.MoveNext();
        skipList.Add(1);
        Assert.Throws<InvalidOperationException>(() => enumerator.MoveNext());
        Assert.Throws<InvalidOperationException>(() => enumerator.Reset());
    }
}