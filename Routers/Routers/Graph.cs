namespace Routers;

using Routers.Exceptions;

/// <summary>
/// Class realizes undirected graph abstraction.
/// </summary>
public class Graph
{
    private int componetsCount;

    private UnionFind conectivityCheck = new ();

    /// <summary>
    /// Gets the number of verticies in the graph.
    /// </summary>
    public int Size { get => this.AdjacencyLists.Count; }

    /// <summary>
    /// Gets a value indicating whether the graph is connected or not.
    /// </summary>
    public bool IsConnected { get => this.componetsCount == 1; }

    /// <summary>
    /// Gets list of all undirected edges in the graph.
    /// </summary>
    public List<Edge> Edges { get; private set; } = new ();

    /// <summary>
    /// Gets Dictionary that contains all pair of neighbours and weight of the edge to that neighbour for all verticies in the graph.
    /// </summary>
    public Dictionary<int, List<(int secondVertex, int Weight)>> AdjacencyLists { get; private set; } = new ();

    /// <summary>
    /// Adds undirected edge secondVertex the graph.
    /// </summary>
    /// <param name="firstVertex">One of the ends of the edge.</param>
    /// <param name="secondVertex">Another end of the edge.</param>
    /// <param name="weight">Weight of the edge.</param>
    public void AddEdge(int firstVertex, int secondVertex, int weight)
    {
        this.Edges.Add(new Edge(firstVertex, secondVertex, weight));
        this.AddVertex(firstVertex);
        this.AddVertex(secondVertex);
        this.AdjacencyLists[firstVertex].Add((secondVertex, weight));
        this.AdjacencyLists[secondVertex].Add((firstVertex, weight));

        if (this.conectivityCheck.Find(firstVertex) != this.conectivityCheck.Find(secondVertex))
        {
            --this.componetsCount;
        }

        this.conectivityCheck.Union(firstVertex, secondVertex);
    }

    /// <summary>
    /// Adds vertex to the graph.
    /// </summary>
    /// <param name="vertex">Vertex to add.</param>
    public void AddVertex(int vertex)
    {
        if (this.AdjacencyLists.ContainsKey(vertex))
        {
            return;
        }

        this.componetsCount++;
        this.AdjacencyLists[vertex] = new ();
    }

    /// <summary>
    /// Creates maximum spanning tree in the graph if possible.
    /// </summary>
    /// <returns>Graph that contains maximum spanning tree of this graph.</returns>
    /// <exception cref="InvalidOperationException">Thrown if graph is empty.</exception>
    /// <exception cref="GraphIsDisconnectedExpceptionException">Thrown if graph is disconnected.</exception>
    public Graph GetMaxSpanningTree()
    {
        if (this.Size == 0)
        {
            throw new InvalidOperationException("Graph is empty");
        }

        if (!this.IsConnected)
        {
            throw new GraphIsDisconnectedExpceptionException();
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

    /// <summary>
    /// Structure represents edge of the graph.
    /// </summary>
    /// <param name="firstVertex">One of the ends of the edge.</param>
    /// <param name="secondVertex">Another end of the edge.</param>
    /// <param name="weight">Wwieght of the edge.</param>
    public struct Edge(int firstVertex, int secondVertex, int weight)
    {
        /// <summary>
        /// Gets or sets one of the ends of the edge.
        /// </summary>
        public int FirstVertex { get; set; } = firstVertex;

        /// <summary>
        /// Gets or sets of sets another one end of the edge.
        /// </summary>
        public int SecondVertex { get; set; } = secondVertex;

        /// <summary>
        /// Gets or sets weight of the edge.
        /// </summary>
        public int Weight { get; set; } = weight;
    }
}