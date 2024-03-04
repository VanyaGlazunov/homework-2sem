using NUnit.Framework.Constraints;
using NUnit.Framework.Internal;

namespace StackCalculator.Tests;


public class CalculatorTests
{
    const float Delta = 1e-12f; 

    private static IEnumerable<TestCaseData> Calculator()
    {
        yield return new TestCaseData(new StackCalculator(new ListStack()));
        yield return new TestCaseData(new StackCalculator(new LinkedListStack()));
    }    

    [TestCaseSource(nameof(Calculator))]
    public void Calculate_CorrectExpression_ReturnsTrueAndExpectedResult(StackCalculator calculator)
    {
        var expression = "1 2 + 5 *";
        var expected = 15f;
        var (actual, isCorrect) = calculator.Calculate(expression);
        Assert.That(isCorrect && Math.Abs(actual - expected) < Delta);
    }

    [TestCaseSource(nameof(Calculator))]
    public void Calculate_IncorrectExpression_ReturnsFalse(StackCalculator calculator)
    {
        var expression = "1 2 + 5";
        var (actual, isCorrect) = calculator.Calculate(expression);
        Assert.That(!isCorrect);
    }

    [TestCaseSource(nameof(Calculator))]
    public void Calculate_ExpressionWithMultiplycationAndDivision_ReturnsTrueAndExpectedValue(StackCalculator calculator)
    {
        var expression = "6 5 * 3 /";
        var expected = 10f;
        var (actual, isCorrect) = calculator.Calculate(expression);
        Assert.That(isCorrect && Math.Abs(actual - expected) < Delta);
    }

    [TestCaseSource(nameof(Calculator))]
    public void Calculate_ExpressionWithBigNumbers_ReturnsTrueAndExpectedValue(StackCalculator calculator)
    {
        var expression = "1000 2000 + 1000 * 5 /";
        var expected = 600000f;
        var (actual, isCorrect) = calculator.Calculate(expression);
        Assert.That(isCorrect && Math.Abs(actual - expected) < Delta);
    }

    [TestCaseSource(nameof(Calculator))]
    public void Calculate_DivisionByZero_ReturnsFalse(StackCalculator calculator)
    {
        var expression = "1000 2000 + 100 100 - /";
        var (actual, isCorrect) = calculator.Calculate(expression);
        Assert.That(!isCorrect);
    }

    [TestCaseSource(nameof(Calculator))]
    public void Calculate_FloatResult_ReturnsTrueAndExpectedValue(StackCalculator calculator)
    {
        var expression = "1 2 /";
        var expected = 0.5f;
        var (actual, isCorrect) = calculator.Calculate(expression);
        Assert.That(isCorrect && Math.Abs(actual - expected) < Delta);
    }
}
