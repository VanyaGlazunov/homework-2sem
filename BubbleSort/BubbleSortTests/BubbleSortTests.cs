namespace BubbleSort.Tests;

using Moq;

public class Tests
{
    [Test]
    public void BubbleSort_ListOfIntsWithDefaultComparer_ReturnsSortedArray()
    {
        var listInts = new List<int>() { 1, 3101, -10, 100, 0, 0, -1 };
        CollectionAssert.AreEqual(listInts.Order().ToArray(), BubbleSort.Sort(listInts, Comparer<int>.Default));
    }

    [Test]
    public void BubbleSort_ListOfStringsWithDefaultComparer_ReturnsSortedArray()
    {
        var listStrings = new List<string>() { "", " ", "AAA", "AAA", "ZZZ", "0", "-1", "-10"};
        CollectionAssert.AreEqual(listStrings.Order().ToArray(), BubbleSort.Sort(listStrings, Comparer<string>.Default));
    }


    [Test]
    public void BubbleSort_WithEmptyList_ReturnsEmptyArray()
    {
        var emptyList = new List<int>();
        CollectionAssert.AreEqual(Array.Empty<int>(), BubbleSort.Sort(emptyList, Comparer<int>.Default));
    }

    [Test]
    public void BubbleSort_WithCustomComparer_ReturnsExpectedArray()
    {
        var ComparerMock = new Mock<IComparer<int>>();
        ComparerMock.Setup(x => x.Compare(100, 0)).Returns(1);
        ComparerMock.Setup(x => x.Compare(100, -1)).Returns(1);
        ComparerMock.Setup(x => x.Compare(0, -1)).Returns(-1);
        ComparerMock.Setup(x => x.Compare(-1, 100)).Returns(-1);
        var lsit = new List<int> { 100, 0, -1};
        var expected = new int[3] {0, -1, 100};
        CollectionAssert.AreEqual(expected, BubbleSort.Sort(lsit, ComparerMock.Object));
        ComparerMock.Verify(x => x.Compare(100, 0), Times.AtLeastOnce());
        ComparerMock.Verify(x => x.Compare(100, -1), Times.AtLeastOnce());
        ComparerMock.Verify(x => x.Compare(0, -1), Times.AtLeastOnce());
        ComparerMock.Verify(x => x.Compare(-1, 100), Times.AtLeastOnce);
    }
}