namespace Routers;

public class Graph
{
    public bool IsConnected { get => ConectivityCheck.ComponetsCount == 1; }

    private Dictionary<int, List<(int to, int weight)>> AdjancencyMatrix = new (); 

    private List<Edge> Edges = new ();
    
    private UnionFind ConectivityCheck = new ();
    
    public void AddEdge(int from, int to, int weight)
    {
        Edges.Add(new Edge(from, to, weight));
        if (AdjancencyMatrix.ContainsKey(from))
        {
            AdjancencyMatrix[from].Add((to, weight));
        }

        ConectivityCheck.Union(to, from);
    }

    public Graph GetMaxSpanningTree()
    {
        if (!this.IsConnected)
        {
            throw new InvalidOperationException("Graph is disconnected.");
        }

        Graph spanningTree = new (); 
        Edges.Sort((Edge a, Edge b) => b.Weight.CompareTo(a.Weight));
        UnionFind unionFind = new ();
        foreach (var edge in this.Edges)
        {
            if (unionFind.Find(edge.From) != unionFind.Find(edge.To))
            {
                spanningTree.AddEdge(edge.From, edge.To, edge.Weight);
            }
        }

        return spanningTree;
    }

    public void Print(Stream outputStream)
    {
        using StreamWriter streamWriter = new(outputStream);
        foreach (var from in this.AdjancencyMatrix.Keys)
        {
            Console.Write($"from: ");
            foreach (var (to, weight) in this.AdjancencyMatrix[from])
            {
                Console.Write($"{to} ({weight})");
            }
        }
    }

    public struct Edge(int from, int to, int weight)
    {
        public int From { get; set; } = from;

        public int To { get; set; } = to;

        public int Weight { get; set; } = weight;        
    }
}