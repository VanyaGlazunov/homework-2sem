namespace ParseTree;

/// <summary>
/// Interface of the node in Parse Tree.
/// </summary>
public interface IParseTreeNode
{
    /// <summary>
    /// Evaluates subtree that is contained by the node.
    /// </summary>
    /// <returns>Returns result of the evaluation.</returns>
    public double Evaluate();

    /// <summary>
    /// Prints content of the node.
    /// </summary>
    /// <returns>Returns string representing node.</returns>
    public string Print();
}