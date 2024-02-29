namespace StackCalculator;

public class StackCalculator
{
    private const float Delta = 1e-12f;

    private IStack stack;

    public StackCalculator(IStack stack) => this.stack = stack;

    public float Calculate(string expression)
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
                float firstOperand = this.stack.Pop();
                float secondOperand = this.stack.Pop();
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
                            throw new ArgumentException("Expression contains division by zero");
                        }

                        this.stack.Push(secondOperand / firstOperand);
                        break;
                    default:
                        throw new ArgumentException("Incorrect expression");
                }
            }
        }

        return this.stack.Pop();
    }
}