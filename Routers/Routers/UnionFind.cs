namespace Routers;

public class UnionFind
{
    private readonly Dictionary<int, int> parent = new();

    public int ComponetsCount { get; private set; } = 0;

    public int Find(int vertex)
    {
        if (!parent.ContainsKey(vertex))
        {
            parent[vertex] = vertex;
            this.ComponetsCount++;
        }

        while (parent[vertex] != vertex)
        {
            parent[vertex] = Find(parent[vertex]);
        }

        return parent[vertex];
    }

    public void Union(int firstVertex, int secondVertex)
    {
        if (!parent.ContainsKey(firstVertex))
        {
            parent[firstVertex] = firstVertex;
            this.ComponetsCount++;
        } 
        if (!parent.ContainsKey(secondVertex))
        {
            parent[secondVertex] = secondVertex;
            this.ComponetsCount++;
        } 
        
        if (parent[firstVertex] == parent[secondVertex])
        {
            return;
        }

        parent[secondVertex] = firstVertex;
        this.ComponetsCount--;
    }
}