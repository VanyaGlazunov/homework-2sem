namespace ParseTree;

/// <summary>
/// Class realizes operand node of Parse tree.
/// </summary>
/// <param name="operand">Integer number.</param>
public class Operand(int operand) : IParseTreeNode
{
    private int operand = operand;

    /// <inheritdoc/>
    public double Evaluate() => this.operand;

    /// <inheritdoc/>
    public string Print() => this.operand.ToString();
}