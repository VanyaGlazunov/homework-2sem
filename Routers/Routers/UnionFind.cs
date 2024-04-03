namespace Routers;

public class UnionFind
{
    private readonly Dictionary<int, int> parent = new ();

    public int Find(int vertex)
    {
        if (!this.parent.ContainsKey(vertex))
        {
            this.parent[vertex] = vertex;
        }

        return vertex == this.parent[vertex] ? vertex : this.parent[vertex] = this.Find(parent[vertex]);
    }

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