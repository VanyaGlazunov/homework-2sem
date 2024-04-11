namespace SparseVector.Tests;

public class SparseVectorTests
{
    [Test]
    public void IsNullVector_ReturnsExpectedResult()
    {
        var vector1 = new SparseVector(10);
        var vector2 = new SparseVector([(1, 0), (2, 0), (3, 0)]);
        Assert.That(vector1.IsNullVector);
        Assert.That(vector2.IsNullVector);
    }

    [Test]
    public void Subtract_EqualVectors_ReturnsNullVector()
    {
        var vector1 = new SparseVector([(1, 1), (2, 2), (3, 3)]);                
        var vector2 = new SparseVector([(1, 1), (2, 2), (3, 3)]);                
        Assert.That(vector1.Subtract(vector2).IsNullVector);
    }

    [Test]
    public void Add_NullAndNonNullVector_DoesNotChangeNonNullVector()
    {
        var vector1 = new SparseVector(4);
        var vector2 = new SparseVector([(1, 1), (2, 2), (3, 3)]);
        var vector = vector2.Add(vector1);
        var actualVector = vector.Subtract(vector2);
        Assert.That(actualVector.IsNullVector);
    }

    [Test]
    public void ScalarProduct_TwoCorrectVectors_ReturnsExpectedResult()
    {
        var vector1 = new SparseVector([(1, 1), (2, 2), (3, 3)]);
        var vector2 = new SparseVector([(1, 2), (2, 3), (3, 4)]);
        int expectedResult = 19;
        Assert.That(vector1.ScalarProduct(vector2) == expectedResult);
    }
}