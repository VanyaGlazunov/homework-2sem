namespace ParseTree.Tests;

public class Tests
{
    private const double delta = 1e-12;

    private ParseTree parseTree;

    [SetUp]
    public void Setup()
    {
        parseTree = new ();
    }

    [TestCase("(* (+ 1 1) 2)", 4.0)]
    [TestCase("(/ 36 (* (+ 1 5) 4))", 1.5)]
    [TestCase("(+ 7 (* (+ 3 6) (- 2 4)))", -11)]
    public void Evaluate_CorrectExpression_ReturnsCorrectResult(string expression, double expectedResult)
    {
        parseTree.Build(expression);
        Assert.That(Math.Abs(parseTree.Evaluate() - expectedResult), Is.LessThan(delta));
    }

    [TestCase("(* (+ 1 1) 2)")]
    [TestCase("(/ 36 (* (+ 1 5) 4))")]
    [TestCase("(+ 7 (* (+ 3 6) (- 2 4)))")]
    public void Print_CorrectExpression_ReturnsCorrectResult(string expression)
    {
        parseTree.Build(expression);
        Assert.That(parseTree.Print(), Is.EqualTo(expression));
    }

    [TestCase("(aba)")]
    [TestCase("(+ 2 A2)")]
    [TestCase("($ 1 1)")]
    public void Build_ExpressionContainsInvalidChar_ThrowsArgumentException(string expression)
    {
        Assert.Throws<ArgumentException>(() => parseTree.Build(expression));
    }

    [TestCase("(* (2 + 2) (- 3 1)) aaa")]
    [TestCase("(+ 1 1) (- 1 1)")]
    public void Build_ExpressionWithCorrectFirstPartAndInvalidLastPart_ThrowArgumentException(string expression)
    {
        Assert.Throws<ArgumentException>(() => parseTree.Build(expression));        
    }

    [TestCase("(/ 1 0)")]
    [TestCase("(/ 1 (* 3 (- 2 2)))")]
    public void Evaluate_ExpressionContainsDivisionByZero_ThrowsDivideByZeroException(string expression)
    {
        parseTree.Build(expression);
        Assert.Throws<DivideByZeroException>(() => parseTree.Evaluate());
    }

    public void Evaluate_TreeIsEmpty_ThrowsInvalidOperationException()
    {
        Assert.Throws<InvalidOperationException>(() => parseTree.Evaluate());
    }

    [TestCase("(* 1)")]
    [TestCase("(+ 1 (/ 1))")]
    [TestCase("(* (+ 9 9))")]
    public void Evaluate_ExpressionDoesNotContainEnoughOperands_ThrowsInvalidOperationException(string expression)
    {
        parseTree.Build(expression);
        Assert.Throws<InvalidOperationException>(() => parseTree.Evaluate());
    }

    [TestCase("(* 1)")]
    [TestCase("(+ 1 (/ 1))")]
    [TestCase("(* (+ 9 9))")]
    public void Print_ExpressionDoesNotContainEnoughOperands_ThrowsInvalidOperationException(string expression)
    {
        parseTree.Build(expression);;
        Assert.Throws<InvalidOperationException>(() => parseTree.Print());
    }
}