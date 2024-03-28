namespace ParseTree;

/// <summary>
/// Class realizes Parse tree.
/// </summary>
public class ParseTree
{
    private static char[] operations = ['+', '-', '*', '/'];

    private IParseTreeNode? root;

    /// <summary>
    /// Creates string that represents expressoin contained by ParseTree.
    /// </summary>
    /// <returns>String representing expression contained by ParseTree.</returns>
    /// <exception cref="InvalidOperationException">Tree should contain non-empty expression.</exception>
    public string Print()
    {
        if (this.root is null)
        {
            throw new InvalidOperationException("Tree is empty");
        }

        try
        {
            return this.root.Print();
        }
        catch (InvalidOperationException)
        {
            Console.WriteLine("Expression is incorrect");
            throw;
        }
    }

    /// <summary>
    /// Evaluates expression contained by the ParseTree.
    /// </summary>
    /// <returns>Result of the evaluation.</returns>
    /// <exception cref="InvalidOperationException">Tree should contain non-empty expression.</exception>
    public double Evaluate()
    {
        if (this.root is null)
        {
            throw new InvalidOperationException("Tree is empty");
        }

        var result = 0.0;

        try
        {
            result = this.root.Evaluate();
        }
        catch (DivideByZeroException)
        {
            Console.WriteLine($"Expression contains division by zero");
            throw;
        }
        catch (InvalidOperationException)
        {
            Console.WriteLine($"Expression is incorrect");
            throw;
        }
        catch (ArgumentException)
        {
            Console.WriteLine($"Expression contains unknown operator");
            throw;
        }

        return result;
    }

    /// <summary>
    /// Build ParseTree of the given expression.
    /// </summary>
    /// <param name="expression">expression to build ParseTree.</param>
    /// <exception cref="ArgumentException">Expression should be correct and non-empty.</exception>
    public void Build(string expression)
    {
        ArgumentException.ThrowIfNullOrEmpty(expression);

        char[] separators = [' ', '(', ')'];
        var tokens = expression.Split(separators, StringSplitOptions.RemoveEmptyEntries);

        int endIndex = 0;
        try
        {
            (this.root, endIndex) = this.Build(tokens, 0);
        }
        catch (ArgumentException)
        {
            Console.WriteLine("Expression contains invalid charecters");
            throw;
        }

        if (endIndex != tokens.Length)
        {
            throw new ArgumentException($"Expression is incorrect");
        }
    }

    private (IParseTreeNode?, int nextIndex) Build(string[] tokens, int index)
    {
        if (index == tokens.Length)
        {
            return (null, index);
        }

        if (int.TryParse(tokens[index], out int operand))
        {
            return (new Operand(operand), ++index);
        }

        if (tokens[index].Length == 1 && operations.Contains(tokens[index][0]))
        {
            Operator operation = new Operator(tokens[index][0]);
            ++index;
            (operation.LeftChild, index) = this.Build(tokens, index);
            (operation.RightChild, index) = this.Build(tokens, index);
            return (operation, index);
        }

        throw new ArgumentException();
    }
}