namespace StackCalculator;

/// <summary>
/// Class to compute expressions using stack.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="StackCalculator"/> class.
/// </remarks>
/// <param name="stack">Stack to use in StackCalculator.</param>
public class StackCalculator(IStack stack)
{
    private const float Delta = 1e-12f;

    private readonly IStack stack = stack;

    /// <summary>
    /// Method calculates expression in postfix form and returns result.
    /// </summary>
    /// <param name="expression">Expression in postifx form.</param>
    /// <returns>Returns computed result and bool flag that indicates whether method worked correctly.</returns>
    public (float result, bool isCorrect) Calculate(string expression)
    {
        var splittedExpression = expression.Split();
        for (int i = 0; i < splittedExpression.Length; ++i)
        {
            if (int.TryParse(splittedExpression[i], out int operand))
            {
                this.stack.Push(operand);
            }
            else
            {
                var (firstOperand, isFirstPopped) = this.stack.Pop();
                var (secondOperand, isSecondPopped) = this.stack.Pop();
                if (!isFirstPopped || !isSecondPopped)
                {
                    return (0, false);
                }

                switch (splittedExpression[i])
                {
                    case "+":
                        this.stack.Push(firstOperand + secondOperand);
                        break;
                    case "-":
                        this.stack.Push(secondOperand - firstOperand);
                        break;
                    case "*":
                        this.stack.Push(firstOperand * secondOperand);
                        break;
                    case "/":
                        if (Math.Abs(firstOperand) < Delta)
                        {
                            return (0, false);
                        }

                        this.stack.Push(secondOperand / firstOperand);
                        break;
                    default:
                        return (0, false);
                }
            }
        }

        var (result, isPopped) = this.stack.Pop();

        return isPopped && this.stack.IsEmpty ? (result, true) : (0, false);
    }
}
