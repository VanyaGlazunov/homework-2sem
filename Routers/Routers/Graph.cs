namespace Routers;

using Routers.Exceptions;

public class Graph
{
    private int componetsCount;

    private UnionFind conectivityCheck = new ();

    public int Size { get => this.AdjacencyLists.Count; }

    public bool IsConnected { get => this.componetsCount == 1; }

    public List<Edge> Edges { get; private set; } = new ();

    public Dictionary<int, List<(int to, int Weight)>> AdjacencyLists { get; private set; } = new ();

    public void AddEdge(int from, int to, int weight)
    {
        this.Edges.Add(new Edge(from, to, weight));
        this.AddVertex(from);
        this.AddVertex(to);
        this.AdjacencyLists[from].Add((to, weight));
        this.AdjacencyLists[to].Add((from, weight));

        if (this.conectivityCheck.Find(from) != this.conectivityCheck.Find(to))
        {
            --this.componetsCount;
        }

        this.conectivityCheck.Union(from, to);
    }

    public void AddVertex(int vertex)
    {
        if (this.AdjacencyLists.ContainsKey(vertex))
        {
            return;
        }

        this.componetsCount++;
        this.AdjacencyLists[vertex] = new ();
    }

    public Graph GetMaxSpanningTree()
    {
        if (this.Size == 0)
        {
            throw new InvalidOperationException("Graph is empty");
        }

        if (!this.IsConnected)
        {
            throw new NetworkIsDisconnectedExpceptionException();
        }

        Graph spanningTree = new ();
        this.Edges.Sort((Edge a, Edge b) => b.Weight.CompareTo(a.Weight));
        UnionFind unionFind = new ();
        foreach (var edge in this.Edges)
        {
            if (unionFind.Find(edge.FirstVertex) != unionFind.Find(edge.SecondVertex))
            {
                spanningTree.AddEdge(edge.FirstVertex, edge.SecondVertex, edge.Weight);
                unionFind.Union(edge.FirstVertex, edge.SecondVertex);
            }
        }

        return spanningTree;
    }

    public struct Edge(int firstVertex, int secondVertex, int weight)
    {
        public int FirstVertex { get; set; } = firstVertex;

        public int SecondVertex { get; set; } = secondVertex;

        public int Weight { get; set; } = weight;
    }
}