namespace ParseTree;

/// <summary>
/// Class realizes operator node of Parse tree.
/// </summary>
/// <param name="operation">Operation sign.</param>
public class Operator(char operation) : IParseTreeNode
{
    private char operation = operation;

    /// <summary>
    /// Gets or sets reference to the left child of the node.
    /// </summary>
    public IParseTreeNode? LeftChild { get; set; }

    /// <summary>
    /// Gets or sets reference to the right child of the node.
    /// </summary>
    public IParseTreeNode? RightChild { get; set; }

    /// <summary>
    /// Evaluates of subtree of the node.
    /// </summary>
    /// <returns>Result of the operation of the node.</returns>
    /// <exception cref="InvalidOperationException">Every operation needs exactly two arguments.</exception>
    /// <exception cref="DivideByZeroException">Cannot divide by zero.</exception>
    public double Evaluate()
    {
        if (this.LeftChild is null || this.RightChild is null)
        {
            throw new InvalidOperationException();
        }

        return this.operation switch
        {
            '+' => this.LeftChild.Evaluate() + this.RightChild.Evaluate(),
            '-' => this.LeftChild.Evaluate() - this.RightChild.Evaluate(),
            '*' => this.LeftChild.Evaluate() * this.RightChild.Evaluate(),
            '/' => Math.Abs(this.RightChild.Evaluate()) < 1e-12 ? throw new DivideByZeroException() : this.LeftChild.Evaluate() / this.RightChild.Evaluate(),
            _ => throw new InvalidOperationException(),
        };
    }

    /// <inheritdoc/>
    public string Print()
    {
        if (this.LeftChild is null || this.RightChild is null)
        {
            throw new InvalidOperationException();
        }

        return $"({this.operation} {this.LeftChild.Print()} {this.RightChild.Print()})";
    }
}