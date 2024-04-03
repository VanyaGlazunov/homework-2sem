namespace Routers;

/// <summary>
/// Class realizes union-find data structure.
/// </summary>
public class UnionFind
{
    private readonly Dictionary<int, int> parent = new ();

    /// <summary>
    /// Finds the number that assosiated with the set containing given nubmer.
    /// </summary>
    /// <param name="vertex">Number to find in what set it is.</param>
    /// <returns>Number that represents the set of the given nubmer.</returns>
    public int Find(int vertex)
    {
        if (!this.parent.ContainsKey(vertex))
        {
            this.parent[vertex] = vertex;
        }

        return vertex == this.parent[vertex] ? vertex : this.parent[vertex] = this.Find(this.parent[vertex]);
    }

    /// <summary>
    /// Unifies two sets of the given numbers.
    /// </summary>
    /// <param name="firstVertex">Number of the first set.</param>
    /// <param name="secondVertex">Number of the second set.</param>
    public void Union(int firstVertex, int secondVertex)
    {
        firstVertex = this.Find(firstVertex);
        secondVertex = this.Find(secondVertex);
        if (firstVertex == secondVertex)
        {
            return;
        }

        this.parent[secondVertex] = firstVertex;
    }
}