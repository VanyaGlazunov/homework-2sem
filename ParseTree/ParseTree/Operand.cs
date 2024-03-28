namespace ParseTree;

/// <summary>
/// Class realizes operand node of Parse tree.
/// </summary>
/// <param name="operand">Integer number.</param>
public class Operand(int operand) : IParseTreeNode
{
    private int operand = operand;

    /// <inheritdoc/>
    public double Evaluate()
    {
        return this.operand;
    }

    /// <inheritdoc/>
    public string Print()
    {
        return this.operand.ToString();
    }
}