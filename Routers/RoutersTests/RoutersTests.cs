using Routers.Exceptions;

namespace Routers.Tests;

public class RoutersTests
{
    private static int Dfs(Graph graph, int currentVertex, bool[] Marked)
    {
        Marked[currentVertex] = true;

        var sumOfWieghts = 0;
        foreach (var (to, weight) in graph.AdjacencyLists[currentVertex])
        {
            if (!Marked[to])
            {
                sumOfWieghts += weight + Dfs(graph, to, Marked);
            }
        }
    
        return sumOfWieghts;
    }

    private static (bool IsTree, int sumOfWeights) CheckForTree(Graph graph)
    {
        if (!graph.IsConnected || graph.Edges.Count != graph.Size - 1)
        {
            return (false, 0);
        }

        return (true, Dfs(graph, 1, new bool[graph.Size + 1]));
    }

    Graph graph;

    [SetUp]
    public void Setup()
    {
        graph = new ();
    }

    [TestCase("TestFiles/CompleteGraph.txt", 147)]
    [TestCase("TestFiles/OneEdgeGraph.txt", 100)]
    [TestCase("TestFiles/SampleFromTask.txt", 15)]
    [TestCase("TestFiles/Tree.txt", 36)]
    public void GetMaxSpanningTree_ConnectedGraph_ReturnsCorrectTree(string filePath, int expectedSumOfWeights)
    {
        graph = NetworkParser.ParseFromFile(filePath);
        var maxSpanningTree = graph.GetMaxSpanningTree();
        var (isTree, actualSumOfWeights) = CheckForTree(maxSpanningTree);
        Assert.That(isTree is true, $"{maxSpanningTree.IsConnected}, {maxSpanningTree.Size}, {maxSpanningTree.Edges.Count}");
        Assert.That(actualSumOfWeights == expectedSumOfWeights, $"{actualSumOfWeights}, {expectedSumOfWeights}");
    }

    [TestCase("TestFiles/TwoComponents.txt")]
    public void GetMaxSpanningTree_DisconnectedGraph_ThrowsInvalidOperationException(string filePath)
    {
        graph = NetworkParser.ParseFromFile(filePath);
        Assert.Throws<GraphIsDisconnectedException>(() => graph.GetMaxSpanningTree());
    }

    public void GetMaxSpanningTree_EmptyGraph_ThrowsInvalidOperationException()
    {
        Assert.Throws<InvalidOperationException>(() => graph.GetMaxSpanningTree());
    }

    [TestCase("TestFiles/NoWeights.txt")]
    [TestCase("TestFiles/MissingEdge.txt")]
    [TestCase("TestFiles/NoColons.txt")]
    public void ParseFromFile_IncorrectNetwork_ThrowsIncorrectNetworkFormatException(string filePath)
    {
        Assert.Throws<IncorrectNetworkFormatException>(() => NetworkParser.ParseFromFile(filePath));
    }

    [TestCase("TestFiles/CompleteGraph.txt")]
    [TestCase("TestFiles/OneEdgeGraph.txt")]
    [TestCase("TestFiles/SampleFromTask.txt")]
    [TestCase("TestFiles/Tree.txt")]
   public void ParseFromFileWriteToFile_CorrectNetwork_WritesExpectedNetwork(string inputFileName)
    {
        var network = NetworkParser.ParseFromFile(inputFileName);
        var outputfile = inputFileName[.. ^4] + "Answer.txt";
        NetworkParser.WriteToFile(outputfile, network);
        var expectedResult = File.ReadAllText(inputFileName);
        var actualResult = File.ReadAllText(outputfile);
    
        Assert.That(actualResult, Is.EqualTo(expectedResult));
    }
}