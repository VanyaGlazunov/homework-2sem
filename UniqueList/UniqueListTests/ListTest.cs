namespace List.UniqueList.Tests;

public class ListTests
{
    private static IEnumerable<TestCaseData> List()
    {
        yield return new TestCaseData(new List<int>());
        yield return new TestCaseData(new UniqueList<int>());
    }

    [TestCaseSource(nameof(List))]
    public void Insert_10DifferentElements_IndexerContainsAndLengthWorksCorrectly(List<int> list)
    {
        for (int i = 0; i < 10; ++i)
        {
            list.Insert(i, i);
        }

        Assert.That(list.Length is 10);

        for (int i = 0; i < 10; ++i)
        {
            Assert.That(list.Contains(i));
        }

        for (int i = 0; i < 10; ++i)
        {
            Assert.That(list[i], Is.EqualTo(i), $"i = {i}, list[i] = {list[i]}");
        }
    }

    [TestCaseSource(nameof(List))]
    public void InsertRemoveAt_10DifferentElements_LengthIsZero(List<int> list)
    {
        for (int i = 0; i < 10; ++i)
        {
            list.Insert(i, i);
        }

        for (int i = 9; i >= 0; --i)
        {
            list.RemoveAt(i);
        }

        Assert.That(list.Length is 0);
    }

    [TestCaseSource(nameof(List))]
    public void InsertAndRemoveAt_OutOfRangeIndex_ThrowsExceptionsIndexOutOfRangeException(List<int> list)
    {
        list.Insert(0, 1);

        Assert.Throws<IndexOutOfRangeException>(() => list.RemoveAt(2));
        Assert.Throws<IndexOutOfRangeException>(() => list.RemoveAt(-1));
        Assert.Throws<IndexOutOfRangeException>(() => list.Insert(2, 10));
        Assert.Throws<IndexOutOfRangeException>(() => list.Insert(-1, 100));
    }

    [TestCaseSource(nameof(List))]
    public void Indexer_OutOfRangeIndex_ThrowsExceptionIndexOutOfRangeException(List<int> list)
    {
        Assert.That(() => list[-1], Throws.Exception.TypeOf<IndexOutOfRangeException>());
        Assert.That(() => list[10], Throws.Exception.TypeOf<IndexOutOfRangeException>());
        Assert.That(() => list[0], Throws.Exception.TypeOf<IndexOutOfRangeException>());
        list.Insert(0, 1);
        list.RemoveAt(0);
        Assert.That(() => list[0], Throws.Exception.TypeOf<IndexOutOfRangeException>());
    }
}