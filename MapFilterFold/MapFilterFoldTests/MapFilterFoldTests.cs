namespace MapFilterFold.Tests;

public class Tests
{
    [Test]
    public void Map_InputTypeAndOutputTypeAreString_ReturnsExpectedResult()
    {
        var initialList = new List<string> { "123", "0001", "00", "010" };
        var mapFunc = (string s) => s[..1];
        var actualList = Functions.Map(initialList, mapFunc);
        var expectedList = new List<string> {"1", "0", "0", "0" };
        Assert.That(actualList, Is.EqualTo(expectedList));
    }

    [Test]
    public void Map_InputTypeAndOutputTypeAreInt_ReturnsExpectedResult()
    {
        var initialList = new List<int> { 123, 100001, 0, -100 };
        var mapFunc = (int i) => i * 10;
        var actualList = Functions.Map(initialList, mapFunc);
        var expectedList = new List<int> {1230, 1000010, 0, -1000 };
        Assert.That(actualList, Is.EqualTo(expectedList));
    }

    [Test]
    public void Map_InputTypeisStringOutputTypeIsInt_ReturnsExpectedResult()
    {
        var initialList = new List<string> { "123", "0001", "00", "-10" };
        var mapFunc = (string s) => int.Parse(s);
        var actualList = Functions.Map(initialList, mapFunc);
        var expectedList = new List<int> {123, 1, 0, -10 };
        Assert.That(actualList, Is.EqualTo(expectedList));
    }

    [Test]
    public void Map_EmeptyList_ReturnsEmptyList()
    {
        Assert.That(Functions.Map([], (int i) => i), Is.Empty);
    }

    [Test]
    public void Filter_InputTypeIsInt_ReturnsExpectedResult()
    {
        var initialList = new List<int> {0, 1, 2, 100, -1, 3 };
        var filterFunc = (int i) => i % 2 == 0;
        var actualList = Functions.Filter(initialList, filterFunc);
        var expectedList = new List<int> { 0, 2, 100 };
        Assert.That(actualList, Is.EqualTo(expectedList));
    }

    [Test]
    public void Filter_EmptyList_ReturnsEmptyList()
    {
        Assert.That(Functions.Filter(new List<int>(), (int i) => true), Is.Empty);
    }

    [Test]
    public void Fold_SumOfInts_ReturnsExpectedResult()
    {
        var initialList = new List<int> {1, 2, 3, 1000, 100000, -1};
        var foldFunc = (int accumulated, int nextValue) => accumulated + nextValue;
        var actualSum = Functions.Fold(initialList, 0, foldFunc);
        var expectedSum = 101005;
        Assert.That(actualSum == expectedSum); 
    }

    [Test]
    public void Fold_SumOfStrings_ReturnsExpectedResult()
    {
        var initialList = new List<string> {"1", "2", "aba", "-1"};
        var foldFunc = (string accumulated, string nextValue) => accumulated + nextValue;
        var actualSum = Functions.Fold(initialList, "", foldFunc);
        var expectedSum = "12aba-1";
        Assert.That(actualSum == expectedSum);
    }

    [Test]
    public void Fold_EmptyList_ReturnsInitialValue()
    {
        Assert.That(Functions.Fold(new List<int> (), -1, (int a, int b) => a + b) == -1);
        Assert.That(Functions.Fold(new List<string> (), "-1", (string a, string b) => a + b) == "-1");
    }
}