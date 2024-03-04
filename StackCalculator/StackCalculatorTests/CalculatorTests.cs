namespace StackCalculator.Tests;


public class CalculatorTests
{
    const float Delta = 1e-12f; 
    StackCalculator stackCalculatorWithListStack;
    StackCalculator stackCalculatorWithLinkedListStack;

    [SetUp]
    public void Setup()
    {
        stackCalculatorWithListStack = new (new ListStack());
        stackCalculatorWithLinkedListStack = new (new LinkedListStack());
    }

    [Test]
    public void Calculate_CorrectExpression_ReturnsTrueAndExpectedResult()
    {
        var expression = "1 2 + 5 *";
        var expected = 15f;
        var (actual, isCorrect) = stackCalculatorWithLinkedListStack.Calculate(expression);
        Assert.That(isCorrect && Math.Abs(actual - expected) < Delta);
        (actual, isCorrect) = stackCalculatorWithListStack.Calculate(expression);
        Assert.That(isCorrect && Math.Abs(actual - expected) < Delta);
    }

    [Test]
    public void Calculate_IncorrectExpression_ReturnsFalse()
    {
        var expression = "1 2 + 5";
        var (actual, isCorrect) = stackCalculatorWithLinkedListStack.Calculate(expression);
        Assert.That(!isCorrect);
        (actual, isCorrect) = stackCalculatorWithListStack.Calculate(expression);
        Assert.That(!isCorrect);
    }

    [Test]
    public void Calculate_ExpressionWithMultiplycationAndDivision_ReturnsTrueAndExpectedValue()
    {
        var expression = "6 5 * 3 /";
        var expected = 10f;
        var (actual, isCorrect) = stackCalculatorWithLinkedListStack.Calculate(expression);
        Assert.That(isCorrect && Math.Abs(actual - expected) < Delta);
        (actual, isCorrect) = stackCalculatorWithListStack.Calculate(expression);
        Assert.That(isCorrect && Math.Abs(actual - expected) < Delta);
    }

    [Test]
    public void Calculate_ExpressionWithBigNumbers_ReturnsTrueAndExpectedValue()
    {
        var expression = "1000 2000 + 1000 * 5 /";
        var expected = 600000f;
        var (actual, isCorrect) = stackCalculatorWithLinkedListStack.Calculate(expression);
        Assert.That(isCorrect && Math.Abs(actual - expected) < Delta);
        (actual, isCorrect) = stackCalculatorWithListStack.Calculate(expression);
        Assert.That(isCorrect && Math.Abs(actual - expected) < Delta);
    }

    [Test]
    public void Calculate_DivisionByZero_ReturnsFalse()
    {
        var expression = "1000 2000 + 100 100 - /";
        var (actual, isCorrect) = stackCalculatorWithLinkedListStack.Calculate(expression);
        Assert.That(!isCorrect);
        (actual, isCorrect) = stackCalculatorWithListStack.Calculate(expression);
        Assert.That(!isCorrect);
    }

    [Test]
    public void Calculate_FloatResult_ReturnsTrueAndExpectedValue()
    {
        var expression = "1 2 /";
        var expected = 0.5f;
        var (actual, isCorrect) = stackCalculatorWithLinkedListStack.Calculate(expression);
        Assert.That(isCorrect && Math.Abs(actual - expected) < Delta);
        (actual, isCorrect) = stackCalculatorWithListStack.Calculate(expression);
        Assert.That(isCorrect && Math.Abs(actual - expected) < Delta);
    }
}
